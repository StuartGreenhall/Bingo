using System.Collections.Generic;
using Bingo.Web.Models;
using NUnit.Framework;
using Bingo.Services;
using System.Linq;

namespace Integration.Tests
{
    [TestFixture]
    public class BingResultsParserTest
    {
        [Test]
        public void ItGeneratesTheSearchOutcomeFromAStringOfHtmlWithAdsIn() 
        {
            var exampleHtml = System.IO.File.ReadAllText(@"Fixtures/example-bing-search-results.html");

            var parser = new BingResultsParser();

            SearchOutcome result = parser.parse(exampleHtml);
        
            Assert.That(result.TotalResultsCount, Is.EqualTo(231000)); 
            Assert.That(result.PageResultCount, Is.EqualTo(9)); 
            Assert.That(result.AdCount, Is.EqualTo(1)); 
            
            Assert.That(result.SearchResults.First().Header, Is.EqualTo("Web Agency in London - We specialise in Web Development."));
            Assert.That(result.SearchResults.First().Summary, Does.Contain("We specialise in Web Development. Send us more info"));
            Assert.That(result.SearchResults.First().Link, Does.Contain("https://0.r.bat.bing.com/?ld=d3p-jIvKQqnmQ1rtIl9u0O3jVUCUw7WEoDjnDJ6OtW2VfJpty-j0ir0j7HhmdGDwjcWoT8tm0mlZgvkHXyEjisfwvKOALNSv_VLEe7b5ohE9iBmqZ__aMQmlUA806MwRKjHKJbaAmnQGtIL2ZBlbEK4dkzx81n1ZFR63nuOjF-4JC-sdJ5&amp;"));
            Assert.That(result.SearchResults.First().IsAd, Is.True);
        }

        [Test]
        public void ItGeneratesTheSearchOutcomeFromAStringOfHtmlWithNoAdsIn() 
        {
            var exampleHtml = System.IO.File.ReadAllText(@"Fixtures/example-bing-search-results-no-ads.html");

            var parser = new BingResultsParser();

            SearchOutcome result = parser.parse(exampleHtml);
        
            Assert.That(result.TotalResultsCount, Is.EqualTo(81500)); 
            Assert.That(result.PageResultCount, Is.EqualTo(10)); 
            Assert.That(result.AdCount, Is.EqualTo(0)); 
        }

        [Test]
        public void ItGeneratesTheSearchOutcomeFromABingSearchThatReturnsNoResults() 
        {
            var exampleHtml = System.IO.File.ReadAllText(@"Fixtures/example-bing-search-no-results.html");

            var parser = new BingResultsParser();

            SearchOutcome result = parser.parse(exampleHtml);
        
            Assert.That(result.TotalResultsCount, Is.EqualTo(0)); 
            Assert.That(result.PageResultCount, Is.EqualTo(0)); 
            Assert.That(result.AdCount, Is.EqualTo(0)); 
            Assert.That(result.SearchResults, Is.EquivalentTo(new List<SearchResult>()));
        }
    }
}
