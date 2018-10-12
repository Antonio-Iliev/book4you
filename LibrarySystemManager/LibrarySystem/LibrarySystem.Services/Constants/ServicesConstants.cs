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
        internal const int MinUserNameLength = 2;
        internal const int MaxUserNameLength = 20;
        internal const int MinTownNameLength = 1;
        internal const int MaxTownNameLength = 50;
        internal const int MinAddressNameLength = 1;
        internal const int MaxAddressNameLength = 50;
        internal const int MinBookInStore = 1;
        internal const int MaxBookInStore = 150;
    }
}
