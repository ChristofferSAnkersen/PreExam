using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PreExam.Models;
using PreExam.Services;

namespace PreExam.Controllers
{
    public class AuctionItemsController : Controller
    {
        private readonly AuctionDbContext _context;
        private readonly AuctionProxy _proxy;

        public AuctionItemsController(AuctionDbContext context, AuctionProxy proxy)
        {
            _context = context;
            _proxy = proxy;
        }

        // GET: AuctionItems
        public async Task<IActionResult> Index()
        {
            return View(await _proxy.GetAllAsync());
        }

        // GET: AuctionItems/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _proxy.GetDetailsAsync(id);
            if (auctionItem == null)
            {
                return NotFound();
            }

            return View(auctionItem);
        }

        // POST: AuctionItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ItemNumber,ItemDescription,RatingPrice,BidPrice,BidCustomName,BidCustomePhone,BidTimeStamp")] AuctionItem auctionItem)
        {
            if (auctionItem.ItemNumber == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bidResponse = await _proxy.ProvideBid(auctionItem);
                                        
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!AuctionItemExists(auctionItem.ItemNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        BadRequest(e);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(auctionItem);
        }

        // GET: AuctionItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuctionItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemNumber,ItemDescription,RatingPrice,BidPrice,BidCustomName,BidCustomePhone,BidTimeStamp")] AuctionItem auctionItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auctionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auctionItem);
        }

        // GET: AuctionItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItems.FindAsync(id);
            if (auctionItem == null)
            {
                return NotFound();
            }
            return View(auctionItem);
        }

        

        // GET: AuctionItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auctionItem = await _context.AuctionItems
                .FirstOrDefaultAsync(m => m.ItemNumber == id);
            if (auctionItem == null)
            {
                return NotFound();
            }

            return View(auctionItem);
        }

        // POST: AuctionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auctionItem = await _context.AuctionItems.FindAsync(id);
            _context.AuctionItems.Remove(auctionItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuctionItemExists(int id)
        {
            return _context.AuctionItems.Any(e => e.ItemNumber == id);
        }
    }
}
