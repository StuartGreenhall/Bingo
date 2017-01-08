using System.Collections.Generic;
using Bingo.Web.Models;
using Bingo.Web.OutputFormatters;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class SearchOutcomeCSVFromatterTests
    {
        [Test]
        public void ConvertsASearchOutcomeIntoACsvString() 
        {   
              var results = new List<SearchResult> {
                new SearchResult { 
                    Type = ResultType.Ad,
                    Link = "www.bing.co.uk",
                    Header = "test1",
                    Summary = "test 1 summary"
                },
                new SearchResult { 
                    Type = ResultType.Natural,
                    Link = "www.bing.co.uk",
                    Header = "test2",
                    Summary = "test 2 summary"
                },
                new SearchResult { 
                    Type = ResultType.Natural,
                    Link = "www.bing.co.uk",
                    Header = "test3",
                    Summary = "test 3 summary"
                },
            };

            var searchOutcome = new SearchOutcome("100 results", results);
            var formatter = new SearchOutcomeCVSFormatter();
            
            var expectedCsv = "Type, Header, Link, Summary\n" 
                              + "Ad, test1, www.bing.co.uk, test 1 summary\n"
                              + "Natural, test2, www.bing.co.uk, test 2 summary\n"
                              + "Natural, test3, www.bing.co.uk, test 3 summary\n";

            var actualCsv = formatter.Convert(searchOutcome); 
            
            Assert.That(actualCsv, Is.EqualTo(expectedCsv)); 
        }
          
        [Test]
        public void ConvertsASearchOutcomeIntoACsvStringWhenNullsOccurInTheData() 
        {
            var results = new List<SearchResult> {
                new SearchResult { 
                    Type = ResultType.Ad,
                    Link = "www.bing.co.uk",
                    Header = "test1",
                    Summary = "test 1 summary"
                },
                new SearchResult { 
                    Type = ResultType.Unknown,
                },
            };

            var searchOutcome = new SearchOutcome("2 results", results);
            var formatter = new SearchOutcomeCVSFormatter();
            
            var expectedCsv = "Type, Header, Link, Summary\n" 
                              + "Ad, test1, www.bing.co.uk, test 1 summary\n"
                              + "Unknown, , , \n";

            var actualCsv = formatter.Convert(searchOutcome); 
            
            Assert.That(actualCsv, Is.EqualTo(expectedCsv)); 
        }
       
    }
}
