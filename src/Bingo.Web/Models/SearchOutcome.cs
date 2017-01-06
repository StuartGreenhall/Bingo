using System.Collections.Generic;

namespace Bingo.Web.Models
{
    public class SearchOutcome
    {
        public int AdCount  { get; }  
        public int PageResultCount  { get; }
        public int TotalResultsCount  { get; }
        
        //Possibly use enum here or and object if we want to add behaviour to it. 
        public IList<SearchResult> SearchResults  { get; }

        public SearchOutcome(string resultsText, List<SearchResult> searchResults)
        {
            this.TotalResultsCount = GetTotalResults(resultsText);
            this.SearchResults = searchResults;
            this.AdCount = GetAdCount(searchResults);
            this.PageResultCount = GetPageResultCount(searchResults);
        }

        public SearchOutcome()
        {
            this.SearchResults = new List<SearchResult>();
        }

        private int GetPageResultCount(List<SearchResult> searchResults)
        {
            return searchResults.FindAll(search => { return search.IsNatural(); }).Count;
        }

        private int GetTotalResults(string resultsText) {
            var number = resultsText.Split(' ')[0]; 

            if(number.IndexOf(',') > 0) {
                number = number.Remove(number.IndexOf(','), 1);
            }
            //should error check here
            return int.Parse(number);
        }

        private int GetAdCount(List<SearchResult> searchResults)
        {
            return searchResults.FindAll(search => { return search.IsAd(); }).Count;
        }


    }
}