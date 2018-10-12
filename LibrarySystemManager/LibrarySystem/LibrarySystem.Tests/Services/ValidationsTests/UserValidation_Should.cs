using LibrarySystem.Data.Models;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Services.ValidationsTests
{
    [TestClass]
    public class UserValidation_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_When_FirstName_IsNull()
        {
            var validator = new CommonValidations();
            var user = new User()
            {               
                FirstName = null,
                MiddleName = "Ivanov",
                LastName = "Ivanov",
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };
            validator.UserValidation(user.FirstName, user.MiddleName, user.LastName);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_When_MiddleName_IsNull()
        {
            var validator = new CommonValidations();
            var user = new User()
            {
                FirstName = "Ivan",
                MiddleName = null,
                LastName = "Ivanov",
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };
            validator.UserValidation(user.FirstName, user.MiddleName, user.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_When_LastName_IsNull()
        {
            var validator = new CommonValidations();
            var user = new User()
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = null,
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };
            validator.UserValidation(user.FirstName, user.MiddleName, user.LastName);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_When_FirstName_IsMoreThan20()
        {
            var validator = new CommonValidations();
            var user = new User()
            {
                FirstName = new string('a', 21),
                MiddleName = "Ivanov",
                LastName = "Ivanov",
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };

            validator.UserValidation(user.FirstName, user.MiddleName, user.LastName);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_When_MiddleName_IsMoreThan20()
        {
            var validator = new CommonValidations();
            var user = new User()
            {
                FirstName = "Ivan",
                MiddleName = new string('a', 21),
                LastName = "Ivanov",
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };

            validator.UserValidation(user.FirstName, user.MiddleName, user.LastName);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_When_LastName_IsMoreThan20()
        {
            var validator = new CommonValidations();
            var user = new User()
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = new string('a', 21),
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };

            validator.UserValidation(user.FirstName, user.MiddleName, user.LastName);
        }
        //
        //
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_When_FirstName_IsLessThan1()
        {
            var validator = new CommonValidations();
            var user = new User()
            {
                FirstName ="a",
                MiddleName = "Ivanov",
                LastName = "Ivanov",
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };

            validator.UserValidation(user.FirstName, user.MiddleName, user.LastName);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_When_MiddleName_IsLessThan1()
        {
            var validator = new CommonValidations();
            var user = new User()
            {
                FirstName = "Ivan",
                MiddleName = "a",
                LastName = "Ivanov",
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };

            validator.UserValidation(user.FirstName, user.MiddleName, user.LastName);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_When_LastName_IsLessThan1()
        {
            var validator = new CommonValidations();
            var user = new User()
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "a",
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };

            validator.UserValidation(user.FirstName, user.MiddleName, user.LastName);
        }

    }
}
