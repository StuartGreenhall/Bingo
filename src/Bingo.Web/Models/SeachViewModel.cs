using System.ComponentModel.DataAnnotations;

namespace Bingo.Web.Models
{
    public class SearchViewModel
    {
        [Required]
        [Display(Name = "Search Term")]
        public string SearchTerm { get; set; }
    }
}