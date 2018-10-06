using LibrarySystem.ConsoleClient.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class AddBookCommand : ICommand
    {
        public string Execute(IList<string> parameters)
        {
            var bookTitle = "Pesho";
            return $"New book {bookTitle} was added.";
        }
    }
}
