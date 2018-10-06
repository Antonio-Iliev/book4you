using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.ConsoleClient.Core.Contracts
{
    public interface IRenderer
    {
        IEnumerable<string> Input();

        void Output(IEnumerable<string> output);

    }
}
