using System;
using System.Collections.Generic;
using Bingo.Web.Models;
using Bingo.Web.OutputFormatters;
using Microsoft.AspNetCore.Mvc.Formatters;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class CSVOutputFormatterTests
    {
        [Test]
        [Ignore("Due to large dependency chain to write up and I generally hate to mock out this much of the chain.")]
        public void ItReturnsTrueWhenCanParseAObjectAndContentTypeIsCsv() 
        {   
              var registry = new Dictionary<Type, IConvertTypeToCSV>();
              var typeOfObject = new Object().GetType();

              registry.Add(new Object().GetType(), new StubConverter());

              var formatter = new CSVOutputFormatter(registry);

            // var outputFormatterCanWriteContext = new OutputFormatterWriteContext(new HttpContext(), , typeOfObject, new Object());
            // Assert.That(formatter.CanWriteResult(outputFormatterCanWriteContext), Is.True);
        }

        [Test]
        [Ignore("Due to large dependency chain to write up and I generally hate to mock out this much of the chain.")]
        public void ItReturnsFalseWhenContentTypeIsCsvButFormatterNotRegistered() 
        {   
            var registry = new Dictionary<Type, IConvertTypeToCSV>();
            registry.Add(new Object().GetType(), new StubConverter());

            var formatter = new CSVOutputFormatter(registry);

            // var outputFormatterCanWriteContext = new OutputFormatterWriteContext(new HttpContext(), , typeOfObject, new Object());
            // Assert.That(formatter.CanWriteResult(outputFormatterCanWriteContext), Is.True);
            
            // Assert.That(formatter.CanWriteResult(outputFormatterCanWriteContext), Is.True);
        }
          
    }
    public class StubConverter : IConvertTypeToCSV
    {
        public string Convert(object objectToConvert)
        {
            return "Converted :)";
        }
    }

}
