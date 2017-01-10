using System.Threading.Tasks;
using NUnit.Framework;
using Bingo.Services;
using System.Net.Http;
using RichardSzalay.MockHttp;
using System.Linq;
using System;

namespace Unit.Tests
{
    [TestFixture]
    public class BingSearchServiceTests
    {
        [Test]
        public async Task ItShouldWork() 
        {
            var exampleHtml = System.IO.File.ReadAllText(@"Fixtures/example-bing-search-results.html");
            var mockHttp = new MockHttpMessageHandler();
            
            mockHttp.When("http://www.bing.com/search?q=adasd")
                    .Respond("text/html", exampleHtml); 

            var httpClient = new HttpClient(mockHttp);

            var bing = new BingSearchService(httpClient);
            var result = await bing.Search("adasd");

            Assert.That(result.TotalResultsCount, Is.EqualTo(231000)); 
            Assert.That(result.PageResultCount, Is.EqualTo(9)); 
            Assert.That(result.AdCount, Is.EqualTo(1)); 
            
            Assert.That(result.SearchResults.First().Header, Is.EqualTo("Web Agency in London - We specialise in Web Development."));
            Assert.That(result.SearchResults.First().Summary, Does.Contain("We specialise in Web Development. Send us more info"));
            Assert.That(result.SearchResults.First().Link, Does.Contain("https://0.r.bat.bing.com/?ld=d3p-jIvKQqnmQ1rtIl9u0O3jVUCUw7WEoDjnDJ6OtW2VfJpty-j0ir0j7HhmdGDwjcWoT8tm0mlZgvkHXyEjisfwvKOALNSv_VLEe7b5ohE9iBmqZ__aMQmlUA806MwRKjHKJbaAmnQGtIL2ZBlbEK4dkzx81n1ZFR63nuOjF-4JC-sdJ5&amp;"));
            Assert.That(result.SearchResults.First().IsAd, Is.True);
        }
    }
}
