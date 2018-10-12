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

            validator.AddressValidation(null, 1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidAddressServiceParametersExeption))]
        public void Throw_When_StreetAddresss_IsEmpty()
        {
            var validator = new CommonValidations();
            var town = new Town();

            validator.AddressValidation("", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAddressServiceParametersExeption))]
        public void Throw_When_StreetAddress_IsMoreThan50()
        {
            var validator = new CommonValidations();
            var town = new Town();

            validator.AddressValidation(new string('a', 51), 1);
        }
    }
}
