using System.Collections.Generic;
using Bingo.Web.Models;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class SearchOutcomeTests
    {
        [Test]
        public void ItCalulatesTotalNumberOfResultsFromString() 
        {
            var resultsText = "911,000 Results";
            var outcome = new SearchOutcome(resultsText, new List<SearchResult>());
    
            Assert.That(outcome.TotalResultsCount, Is.EqualTo(911000)); 
        }
          
        [Test]
        public void ItCalulatesTotalNumberOfResultsFromStringWithNoCommaIn() 
        {
            var resultsText = "120 Results";
            var outcome = new SearchOutcome(resultsText, new List<SearchResult>());
    
            Assert.That(outcome.TotalResultsCount, Is.EqualTo(120)); 
        }

        [Test]
        public void ItCalulatesTotalNumberOfResultsFromStringWithManyCommasIn() 
        {
            var resultsText = "120,000,000 Results";
            var outcome = new SearchOutcome(resultsText, new List<SearchResult>());
    
            Assert.That(outcome.TotalResultsCount, Is.EqualTo(120000000)); 
        }

        [Test]
        public void ItCalulatesAdCountOnPageFromSearchResults() 
        {   
            var results = new List<SearchResult> {
                new SearchResult { Type = ResultType.Ad },
                new SearchResult { Type = ResultType.Ad },
                new SearchResult { Type = ResultType.Natural }
            };

            var outcome = new SearchOutcome("110", results);
            Assert.That(outcome.AdCount, Is.EqualTo(2)); 
        }

        [Test]
        public void ItCalulatesResultCountOnPageFromSearchResults() 
        {   
            var results = new List<SearchResult> {
                new SearchResult { Type = ResultType.Natural },
                new SearchResult { Type = ResultType.Natural },
                new SearchResult { Type = ResultType.Unknown }
            };

            var outcome = new SearchOutcome("110", results);
            Assert.That(outcome.AdCount, Is.EqualTo(0)); 
            Assert.That(outcome.PageResultCount, Is.EqualTo(2)); 
        }

        
    }
}
