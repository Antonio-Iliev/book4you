using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands.Contracts
{
    public interface ICommand
    {
        string Execute(IEnumerable<string> parameters);       
    }
}
