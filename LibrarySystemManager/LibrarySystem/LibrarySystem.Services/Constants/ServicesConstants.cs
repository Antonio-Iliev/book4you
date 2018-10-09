namespace LibrarySystem.Services.Constants
{
    internal static class ServicesConstants
    {
        // Error messages
        internal const string InvalidNumbersOfParameters = "Invalid numbers of parameters";
        internal const string UserAlreadyExists = "This user already exists";
        internal const string UserDoesNotExist = "This user does not exist";
        internal const string BookDoesNotExist = "This user does not exist";
        internal const string NoUsersFound= "No users found";

        // Constraints
        internal const int MinBookTitleLength = 2;
        internal const int MaxBookTitleLength = 50;
        internal const int MinGenreNameLength = 2;
        internal const int MaxGenreNameLength = 50;
        internal const int MinAuthorNameLength = 2;
        internal const int MaxAuthorNameLength = 50;
        internal const int MinUserNameLength = 1;
        internal const int MaxUserNameLength = 20;

        //Commands
        //AddUser, Ivanov, Peshov, Peshov, 0889257125, ul.Dran dran, Sofia
        //getuser, Gosho, Goshov, Goshov
        //AddUser, Pesho, Peshov, Peshov, 0889257125, ul."Dran dran", Dupnitsa
        //AddBook, Ala Bala, Horror, Pesho 3
        //AddBook, A Clash, Drama, Dr Radeva 5
    }
}
