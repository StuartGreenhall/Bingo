using System;
using System.Collections.Generic;

namespace Bingo.Web.Models
{
    public class SearchOutcome
    {
        public int AdCount  { get; }  
        public int PageResultCount  { get; }
        public int TotalResultsCount  { get; }
        public String SearchTerm  { get; private set; }
        public List<SearchResult> SearchResults  { get; }

        public SearchOutcome(String resultsText, List<SearchResult> searchResults)
        {
            this.TotalResultsCount = GetTotalResults(resultsText);
            this.SearchResults = searchResults;
            this.AdCount = GetAdCount(searchResults);
            this.PageResultCount = GetPageResultCount();
        }

        public SearchOutcome()
        {
            this.SearchResults = new List<SearchResult>();
        }

        public void WasFor(String searchTerm)
        {
            this.SearchTerm = searchTerm;
        }

        private int GetPageResultCount()
        {
            return GetNaturalSearchResults().Count;
        }

        public List<SearchResult> GetNaturalSearchResults() {
            return this.SearchResults.FindAll(search => { return search.IsNatural(); });
        }

        public List<SearchResult> GetAdSearchResults() {
            return this.SearchResults.FindAll(search => { return search.IsAd(); });
        }

        private int GetTotalResults(string resultsText) {
            var number = resultsText.Split(' ')[0]; 
            number = number.Replace(@",", string.Empty);
            return int.Parse(number);
        }

        private int GetAdCount(List<SearchResult> searchResults)
        {
            return searchResults.FindAll(search => { return search.IsAd(); }).Count;
        }

    }
}