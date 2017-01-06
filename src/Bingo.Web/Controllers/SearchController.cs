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

        public async Task<IActionResult> IndexAsync(SearchViewModel vm)
        {
            var outcome = await SearchEngine.Search(vm.SearchTerm); 
            return View(outcome);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}