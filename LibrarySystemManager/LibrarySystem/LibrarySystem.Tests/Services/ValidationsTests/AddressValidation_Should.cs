using LibrarySystem.Data.Models;
using LibrarySystem.Services.Exceptions.AddressServices;
using LibrarySystem.Services.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Services.ValidationsTests
{
    [TestClass]
    public class AddressValidation_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_When_StreetAddress_IsNull()
        {
            var validator = new CommonValidations();
            var town = new Town();

            validator.AddressValidation(null, town);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_When_Town_IsNull()
        {
            var validator = new CommonValidations();
            var town = new Town();

            validator.AddressValidation("test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAddressServiceParametersExeption))]
        public void Throw_When_StreetAddresss_IsEmpty()
        {
            var validator = new CommonValidations();
            var town = new Town();

            validator.AddressValidation("", town);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAddressServiceParametersExeption))]
        public void Throw_When_StreetAddress_IsMoreThan50()
        {
            var validator = new CommonValidations();
            var town = new Town();

            validator.AddressValidation(new string('a', 51), town);
        }
    }
}
