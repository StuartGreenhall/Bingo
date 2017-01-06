using System;
using System.Threading.Tasks;
using System.Net.Http;
using Bingo.Web.Models;

namespace Bingo.Services
{
    public class BingSearchService : ISearchEngine
    {
        private static HttpClient Client = new HttpClient {
            BaseAddress = new Uri("http://www.bing.com/search?q=")
        };
        private const String baseUrl = "http://www.bing.com/search?q="; 

        public async Task<SearchOutcome> Search(string SearchTerm)
        {
            var url = baseUrl + SearchTerm;
            var response = await Client.GetAsync(url);
            var results = await response.Content.ReadAsStringAsync();

            // Console.WriteLine(results);
            return new SearchOutcome();
        }

    }
}
