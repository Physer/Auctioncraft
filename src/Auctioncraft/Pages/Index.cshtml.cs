using Auctioncraft.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Auctioncraft.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public IEnumerable<Item> TopAuctions { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public void OnGet()
        {
            try
            {
                var auctions = GetOrCreateDailyDatabase();
                TopAuctions = auctions.OrderByDescending(auction => auction.RegionAvgDailySold).Take(10);
            }
            catch(Exception e)
            {
                return;
            }
        }

        private IEnumerable<Item> GetOrCreateDailyDatabase()
        {
            var items = new HashSet<Item>();
            var fileName = $"/app/auctions-{DateTime.UtcNow:yyyyMMdd}.json";
            if (!System.IO.File.Exists(fileName))
                return items;

            using (StreamReader file = System.IO.File.OpenText(fileName))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                reader.SupportMultipleContent = true;
                var serializer = new JsonSerializer();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var item = serializer.Deserialize<Item>(reader);
                        items.Add(item);
                    }
                }
            }

            return items;
        }
    }
}
