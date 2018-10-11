using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.ConsoleClient.Core.Contracts
{
    public interface ICommandProcessor
    {
        IEnumerable<string>ProcessCommands(IEnumerable<string> commands);
    }
}
