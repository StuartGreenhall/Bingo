using NUnit.Framework;
using WebApplication.Services;

namespace Unit.Tests
{
    [TestFixture]
    public class BingSearchServiceTests
    {
        [Test]
        public void ItShouldWork() 
        {
            var bing = new BingSearchService();
            Assert.True(bing.Search("wibble")); 
        }
    }
}
