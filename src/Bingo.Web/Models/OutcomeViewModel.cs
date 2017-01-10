using System;
using Newtonsoft.Json;

namespace Bingo.Web.Models
{
    public class SearchOutcomeViewModel
    {
        public SearchOutcome Outcome;
        public String Json; 
        
        public SearchOutcomeViewModel(SearchOutcome outcome)
        {
            this.Outcome = outcome;
            this.Json = JsonConvert.SerializeObject(outcome);
        }
    }
}