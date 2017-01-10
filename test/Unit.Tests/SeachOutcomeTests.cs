using System;
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
        public void ItCalulatesComplimentaryResultCountOnPageFromSearchResults() 
        {   
            var results = new List<SearchResult> {
                new SearchResult { Type = ResultType.Natural },
                new SearchResult { Type = ResultType.Natural },
                new SearchResult { Type = ResultType.Complementary },
                new SearchResult { Type = ResultType.Ad },
            };

            var outcome = new SearchOutcome("110", results);
            Assert.That(outcome.AdCount, Is.EqualTo(1)); 
            Assert.That(outcome.ComplimentaryCount, Is.EqualTo(1)); 
            Assert.That(outcome.PageResultCount, Is.EqualTo(2));
        }

                [Test]
        public void ItCalulatesResultDistributionFromSearchResults() 
        {   
            var results = new List<SearchResult> {
                new SearchResult { Type = ResultType.Natural },
                new SearchResult { Type = ResultType.Natural },
                new SearchResult { Type = ResultType.Complementary },
                new SearchResult { Type = ResultType.Ad },
            };

            var outcome = new SearchOutcome("110", results);
            Assert.That(outcome.AdCount, Is.EqualTo(1)); 
            Assert.That(outcome.ComplimentaryCount, Is.EqualTo(1)); 
            Assert.That(outcome.PageResultCount, Is.EqualTo(2));
            Assert.That(outcome.ResultTypeDistribution, Is.EquivalentTo(new int[] {2, 1, 1})); 
        }

        
    }
}
