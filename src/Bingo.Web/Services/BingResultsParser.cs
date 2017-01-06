using System;
using System.Linq;
using HtmlAgilityPack;
using Bingo.Web.Models;
using System.Collections.Generic;

namespace Bingo.Services
{
    public class BingResultsParser
    {
        private const String resultsXPath = "//*[@id=\"b_results\"]/li[*]";
        private const String totalCountXPath = "//*[@id=\"b_tween\"]/span[1]";

        public SearchOutcome parse(String rawHtml)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawHtml);
            
            var results = doc.DocumentNode.SelectNodes(resultsXPath);
            var searchResults = results.Select(CreateSearchResult).ToList();

            if(NoResultsIn(searchResults)) {
                return new SearchOutcome();
            }

            var resultText = doc.DocumentNode.SelectSingleNode(totalCountXPath).InnerText;
            return new SearchOutcome(resultText, searchResults);
            
        }

        private bool NoResultsIn(List<SearchResult> searchResults)
        {
            var noResultCount = searchResults.FindAll(SearchResult => { return SearchResult.IsNoResult(); }).Count;
            if (noResultCount >= 1 && searchResults.Count == noResultCount) return true;
            else return false;
        }

        private SearchResult CreateSearchResult(HtmlNode node)
        {   
            var type = node.Attributes[0].Value;
            switch (type)
            {
                case "b_ad":
                    return BuildFromAd(node);
                case "b_algo":
                    return BuildFromNatural(node);
                case "b_no":
                    return new SearchResult() { Type = ResultType.NoResult };
                default:
                    // push Unknown down to defaul constructor
                    return new SearchResult() { Type = ResultType.Unknown };
            } 
        }

        private SearchResult BuildFromNatural(HtmlNode node)
        {
            var header = node.SelectSingleNode(".//h2").InnerText;
            var summary = node.SelectSingleNode(".//p").InnerText;

            return new SearchResult()
            {
                Type = ResultType.Natural,
                Header = header,
                Summary = summary   
            };
        }

        private SearchResult BuildFromAd(HtmlNode node)
        {
            var header = node.SelectSingleNode(".//h2").InnerText;
            var textRows = node.SelectNodes(".//p").Select(p => { return p.InnerText; });
            
            var summary = string.Join(" ", textRows);

            return new SearchResult()
            {
                Type = ResultType.Ad,
                Header = header,
                Summary = summary   
            };
        }
    }
}
