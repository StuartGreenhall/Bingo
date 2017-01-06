using System.Collections.Generic;
using Bingo.Web.Models;
using NUnit.Framework;
using Bingo.Services;

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
