using LibrarySystem.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
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
            var result = new StringBuilder();
            foreach (var line in output)
            {
                result.AppendLine(line);
            }
            Console.WriteLine(result.ToString().Trim());
        }
    }
}
