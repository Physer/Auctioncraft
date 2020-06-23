using Auctioncraft.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace Auctioncraft.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public void OnGet()
        {
            try
            {
                GetOrCreateDailyDatabase();
            }
            catch(Exception e)
            {
                return;
            }
        }

        private void GetOrCreateDailyDatabase()
        {
            var items = new HashSet<Item>();
            var fileName = $"/app/auctions-{DateTime.UtcNow:yyyyMMdd}.json";
            if (!System.IO.File.Exists(fileName))
                return;
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
        }
    }
}
