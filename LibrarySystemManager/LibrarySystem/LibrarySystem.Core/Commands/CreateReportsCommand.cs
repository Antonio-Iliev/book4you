using Autofac;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class CreateReportsCommand : ICommand
    {
        private readonly IComponentContext autofacContext;

        public CreateReportsCommand(IComponentContext autofacContext)
        {
            this.autofacContext = autofacContext;
        }

        public string Execute(IEnumerable<string> parameters)
        {
            List<string> args = parameters.ToList();

            if (args.Count != 2)
            {
                throw new ArgumentException("Invalid parameters");
            }

            string commandName = args[0].ToLower();
            IEnumerable<string> reportBy = args.Skip(1);
            string infoParameter = args[1].ToLower();

            string exportFolder = @"../../../../Reports/";
            string reportDate = DateTime.Now.ToString("dd-MM-yyyy");
            string exportFile = Path.Combine(exportFolder, $"Report_{commandName}_by_{infoParameter}_{reportDate}.pdf");
            var command = this.autofacContext.ResolveNamed<ICommand>(commandName);

            string print = command.Execute(reportBy);

            using (var writer = new PdfWriter(exportFile))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var doc = new Document(pdf);

                    doc.Add(new Paragraph(print));
                }
            }

            return $"Report about {commandName} by {infoParameter} is ready!";
        }
    }
}
