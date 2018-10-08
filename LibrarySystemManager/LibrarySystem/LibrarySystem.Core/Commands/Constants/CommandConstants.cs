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
        internal const string UserDoesNotExist = "This user does not exist";
        internal const string BookDoesNotExist = "This user does not exist";

        // Constraints
        internal const int MaxBookTitleLength = 50;
        internal const int MaxGenreNameLength = 50;
        internal const int MaxAuthorNameLength = 50;
        internal const int MinUserNameLength = 1;
        internal const int MaxUserNameLength = 20;
        internal const int InitialBookAmount = 10;

        //Commands
        //AddUser, Ivan, Peshov, Peshov, 0889257125, ul.Dran dran, Dupnitsa
        //AddUser, Pesho, Peshov, Peshov, 088117125, ul. A. Malinov, Sofia
        //AddUser, Stoyan, Peshov, Peshov, 0889997125, ul. V. Levski, Plovdiv


    }
}
