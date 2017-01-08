using System;
using System.Text;
using Bingo.Web.Models;

namespace Bingo.Web.OutputFormatters
{
    public class SearchOutcomeCVSFormatter : IConvertTypeToCSV
    {
        public string Convert(object objectToConvert)
        {   
            SearchOutcome outcome = (SearchOutcome) objectToConvert;
            var header = "Type, Header, Link, Summary\n";
            var builder = new StringBuilder();
            builder.Append(header);
            outcome.SearchResults.ForEach(row => {
                builder.Append(String.Format("{0}, {1}, {2}, {3}\n", row.Type, row.Header, row.Link,row.Summary));
            });
            return builder.ToString();
        }
    }
}