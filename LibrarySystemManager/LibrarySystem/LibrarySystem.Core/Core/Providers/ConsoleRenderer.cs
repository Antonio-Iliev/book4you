using LibrarySystem.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Core.Providers
{
    public class ConsoleRenderer : IRenderer
    {
        public IEnumerable<string> Input()
        {
            var currentLine = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(currentLine))
            {
                yield return currentLine;
                currentLine = Console.ReadLine();
            }
        }

        public void Output(IEnumerable<string> output)
        {
            if (output.Count() < 15)
            {
                var result = new StringBuilder();
                foreach (var line in output)
                {
                    result.AppendLine(line);
                }
                Console.WriteLine(result.ToString().Trim());
            }
            else
            {
                while (output.Count() != 0)
                {
                    Console.WriteLine(Environment.NewLine, output.Take(15));
                    output = output.Skip(15);
                    Console.WriteLine("Press any key to load next page.");

                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }
    }
}
