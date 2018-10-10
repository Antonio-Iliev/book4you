using LibrarySystem.Data.Models;

namespace LibrarySystem.Services.Abstract.Contracts
{
    public interface IValidations
    {
        void TownValidation(string townName);

        void AuthorValidation(string authorName);

        void GenreValidation(string genreName);

        void BookTitleValidation(string bookTitle);

        void BookInStoreValidation(int numberOfBooks);

        void AddressValidation(string streetAddress, Town town);

        void UserValidation(string firstName, string middleName, string lastName);
    }
}