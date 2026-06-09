using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services
{
    public class AuctionSvcHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly DB _db;
        public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config, DB db)
        {
            _httpClient = httpClient;
            _config = config;
            _db = db;
        }

        public async Task<List<Item>> GetItemsForSearchDb()
        {
            var lastUpdated = await _db.Find<Item, string>()
                .Sort(x => x.Descending(x => x.UpdatedAt))
                .Project(x => x.UpdatedAt.ToString())
                .ExecuteFirstAsync();

            return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"]
                + "/api/auctions?date=" + lastUpdated);
        }
    }
}
