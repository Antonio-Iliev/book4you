using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Constants;
using LibrarySystem.Services.Exceptions.TownServices;
using System.Linq;
using LibrarySystem.Services.Exceptions.AuthorServices;
using LibrarySystem.Services.Exceptions.GenreServiceExeptions;
using LibrarySystem.Services.Exceptions.BookServiceExeptions;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Exceptions.AddressServices;
using LibrarySystem.Services.Exceptions.UserServices;

namespace LibrarySystem.Services.Validations
{
    public class CommonValidations : IValidations
    {
        private void IsNull<T>(T inputObject)
        {
            if (inputObject == null)
            {
                string objectName = inputObject.GetType().ToString().Split(".").Last();
                throw new AddTownNullableExeption($"{objectName} can not be null!");
            }
        }

        public void TownValidation(string townName)
        {
            IsNull(townName);
            if (townName.Length < ServicesConstants.MinTownNameLength)
            {
                throw new InvalidTownServiceParametersExeption(
                    $"The town name is less then {ServicesConstants.MinTownNameLength} symbol.");
            }
            if (townName.Length > ServicesConstants.MaxTownNameLength)
            {
                throw new InvalidTownServiceParametersExeption(
                    $"The town name is more then {ServicesConstants.MaxTownNameLength} symbols.");
            }
        }

        public void AddressValidation(string streetAddress, Town town)
        {
            IsNull(streetAddress);
            IsNull(town);
            if (streetAddress.Length < ServicesConstants.MinAddressNameLength)
            {
                throw new InvalidAddressServiceParametersExeption
                       ($"Street Address is less then {ServicesConstants.MinAddressNameLength} symbol.");
            }
            if (streetAddress.Length > ServicesConstants.MaxAddressNameLength)
            {
                throw new InvalidAddressServiceParametersExeption
                    ($"Street Address is more then {ServicesConstants.MaxAddressNameLength} symbols.");
            }
        }

        public void AuthorValidation(string authorName)
        {
            IsNull(authorName);
            if (authorName.Length < ServicesConstants.MinAuthorNameLength)
            {
                throw new InvalidAuthorServiceParametersExeption
                    ($"The author name is less then {ServicesConstants.MinAuthorNameLength} symbols.");
            }
            if (authorName.Length > ServicesConstants.MaxAuthorNameLength)
            {
                throw new InvalidAuthorServiceParametersExeption
                    ($"The author name is more then {ServicesConstants.MaxAuthorNameLength} symbols.");
            }
        }

        public void GenreValidation(string genreName)
        {
            IsNull(genreName);
            if (genreName.Length < ServicesConstants.MinGenreNameLength)
            {
                throw new InvalidGenreServiceParametersExeption
                    ($"The genre name is less then {ServicesConstants.MinGenreNameLength} symbols.");
            }
            if (genreName.Length > ServicesConstants.MaxGenreNameLength)
            {
                throw new InvalidGenreServiceParametersExeption
                    ($"The genre name is more then {ServicesConstants.MaxGenreNameLength} symbols.");
            }
        }

        public void BookTitleValidation(string bookTitle)
        {
            IsNull(bookTitle);
            if (bookTitle.Length < ServicesConstants.MinBookTitleLength)
            {
                throw new InvalidBookServiceParametersExeption
                    ($"The title of the book is less then {ServicesConstants.MinBookTitleLength} symbols.");
            }
            if (bookTitle.Length > ServicesConstants.MaxBookTitleLength)
            {
                throw new InvalidBookServiceParametersExeption
                    ($"The title of the book is less then {ServicesConstants.MaxBookTitleLength} symbols.");
            }
        }

        public void BookInStoreValidation(int numberOfBooks)
        {
            if (numberOfBooks < ServicesConstants.MinBookInStore)
            {
                throw new InvalidBookServiceParametersExeption
                    ($"The number of books can't be zero or negative number.");
            }
            if (numberOfBooks > ServicesConstants.MaxBookInStore)
            {
                throw new InvalidBookServiceParametersExeption
                    ($"The number of books can't be more then {ServicesConstants.MaxBookInStore}");
            }
        }

        public void UserValidation(string firstName, string middleName, string lastName)
        {
            IsNull(firstName);
            IsNull(middleName);
            IsNull(lastName);
            if (firstName.Length < ServicesConstants.MinUserNameLength)
            {
                throw new InvalidUserServiceParametersExeption
                      ($"First name is less then {ServicesConstants.MinUserNameLength} symbols.");
            }
            if (firstName.Length > ServicesConstants.MaxUserNameLength)
            {
                throw new InvalidUserServiceParametersExeption
                      ($"First name is more then {ServicesConstants.MaxUserNameLength} symbols.");
            }

            if (middleName.Length < ServicesConstants.MinUserNameLength)
            {
                throw new InvalidUserServiceParametersExeption
                      ($"Middle name is less then {ServicesConstants.MinUserNameLength} symbols.");
            }
            if (middleName.Length > ServicesConstants.MaxUserNameLength)
            {
                throw new InvalidUserServiceParametersExeption
                      ($"Middle name is more then {ServicesConstants.MaxUserNameLength} symbols.");
            }

            if (lastName.Length < ServicesConstants.MinUserNameLength)
            {
                throw new InvalidUserServiceParametersExeption
                      ($"Last name is less then {ServicesConstants.MinUserNameLength} symbols.");
            }
            if (lastName.Length > ServicesConstants.MaxUserNameLength)
            {
                throw new InvalidUserServiceParametersExeption
                      ($"Last name is more then {ServicesConstants.MaxUserNameLength} symbols.");
            }
        }
    }
}
