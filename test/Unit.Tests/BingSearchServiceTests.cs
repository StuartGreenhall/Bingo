using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NUnit.Framework;
using Bingo.Services;

namespace Unit.Tests
{
    [TestFixture]
    public class BingSearchServiceTests
    {
        [Test]
        [Ignore("")]
        public async Task ItShouldWork() 
        {
            var bing = new BingSearchService();
            var result = await bing.Search("wibble");
            System.Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("wobble")); 
        }
    }
}
