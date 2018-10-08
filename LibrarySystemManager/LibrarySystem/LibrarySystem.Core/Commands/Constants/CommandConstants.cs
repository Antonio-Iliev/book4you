using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands.Constants
{
    internal static class CommandConstants
    {
        // Error messages
        internal const string InvalidNumbersOfParameters = "Invalid numbers of parameters";
        internal const string UserAlreadyExists = "This user already exists";

        // Constrain
        internal const int MaxBookTitleLength = 50;
        internal const int MaxGenreNameLength = 50;
        internal const int MaxAuthorNameLength = 50;

        //
        internal const int InitialBookAmount = 10;
    }
}
