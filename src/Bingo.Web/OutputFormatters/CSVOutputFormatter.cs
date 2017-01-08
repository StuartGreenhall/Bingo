using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Bingo.Web.OutputFormatters
{
    public class CSVOutputFormatter : IOutputFormatter
    {
        private const String textCsv = "text/csv"; 
        private IDictionary<Type, IConvertTypeToCSV> registry;

        public CSVOutputFormatter(IDictionary<Type, IConvertTypeToCSV> registry)
        {   
            this.registry = registry;
        }

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {    
            if (context == null) throw new ArgumentNullException(nameof(context));
        
            if (context.ContentType == null 
                || context.ContentType.ToString() == textCsv
                && FormatterRegisteredForType(context.ObjectType)) {
                return true;
            }
            return false;
        }

        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = textCsv;

            if(FormatterRegisteredForType(context.ObjectType))     
            {
                var formatter = registry[context.ObjectType];
                var output = formatter.Convert(context.Object);

                using (var writer = context.WriterFactory(response.Body, Encoding.UTF8))
                {   
                    // Jil.JSON.Serialize(context.Object, writer);
                    // write stuff here.
                    writer.WriteLine(output);
                    await writer.FlushAsync();
                }
            } 
        }

        private bool FormatterRegisteredForType(Type theType) {
            return registry.ContainsKey(theType);
        }
    }
}