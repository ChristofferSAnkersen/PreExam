using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PreExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PreExam.Services
{
    public class AuctionProxy
    {
        //For GetDetailsAsync
        const string baseUrl = "https://localhost:44388/api";

        [HttpGet]
        public async Task<List<AuctionItem>> GetAllAsync()
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("https://localhost:44388/api/auctionitems");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var auctionResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<List<AuctionItem>>(auctionResponse);
        }

        [HttpGet]
        public async Task<AuctionItem> GetDetailsAsync(int id)
        {
            var url = $"{baseUrl}/AuctionItems/{id}";
            var client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<AuctionItem>(json);
        }

        [HttpPut]
        public async Task<AuctionItem> ProvideBid(AuctionItem auctionItem)
        {
            var url = $"{baseUrl}/AuctionItems/{auctionItem.ItemNumber}";
            var client = new HttpClient();
            var json = new StringContent(JsonConvert.SerializeObject(auctionItem), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, json);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var apiResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<AuctionItem>(apiResponse);

        }
    }
}
