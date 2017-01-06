using System;

namespace Bingo.Web.Models
{
    public class SearchResult
    {
        public String Summary  {get; set;}
        public String Header  {get; set;}
        
        //Possibly use enum here or and object if we want to add behaviour to it. 
        public ResultType Type  {get; set;}

        public SearchResult()
        {
            
        }

        public bool IsAd() {
            return Type == ResultType.Ad;
        }

        public bool IsUnknown() {
            return Type == ResultType.Unknown;
        }

        public bool IsNatural() {
            return Type == ResultType.Natural;
        }
        
        public bool IsNoResult()
        {
             return Type == ResultType.NoResult;
        }

        public override String ToString() { 
            return String.Format("{0}\nSummary: {0}\nHeader: {2}", Header, Header); 
        }


    }
}