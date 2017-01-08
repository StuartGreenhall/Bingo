using System;
using System.Threading.Tasks;
using System.Net.Http;
using Bingo.Web.Models;

namespace Bingo.Services
{
    public class BingSearchService : ISearchEngine
    {   
        private BingResultsParser parser = new BingResultsParser();
        private const String baseUrl = "http://www.bing.com/search?q="; 
        private HttpClient client;

        public BingSearchService(HttpClient client)
        {   
            client.BaseAddress = new Uri(baseUrl);
            this.client = client;
        }

        public async Task<SearchOutcome> Search(string searchTerm)
        {  
            var url = baseUrl + searchTerm;
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();

            var outcome = parser.parse(responseBody);
            outcome.WasFor(searchTerm);
            return outcome; 
        }

    }
}
