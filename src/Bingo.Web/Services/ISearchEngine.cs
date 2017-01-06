
using System.Threading.Tasks;
using Bingo.Web.Models;

namespace Bingo.Services
{
    public interface ISearchEngine
    {
        Task<SearchOutcome> Search(string SearchTerm);
    }
}