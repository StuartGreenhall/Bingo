using Microsoft.AspNetCore.Mvc;
using Bingo.Web.Models;
using Bingo.Services;
using System.Threading.Tasks;

namespace Bingo.Controllers
{
    public class SearchController : Controller
    {  
        private ISearchEngine SearchEngine;
        public SearchController(ISearchEngine searchEngine)
        {
            this.SearchEngine = searchEngine;
        }

        public async Task<IActionResult> Index(SearchViewModel vm)
        {
            var outcome = await SearchEngine.Search(vm.SearchTerm); 
            return View(outcome);
        }

        [Produces("text/csv")]
        public async Task<IActionResult> Raw(SearchViewModel vm)
        {   
            var outcome = await SearchEngine.Search(vm.SearchTerm); 
            return new ObjectResult(outcome);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}