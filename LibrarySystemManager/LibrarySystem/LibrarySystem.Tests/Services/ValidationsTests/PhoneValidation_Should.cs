using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibrarySystem.Tests.Services.ValidationsTests
{
    [TestClass]
    public class PhoneValidation_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_When_Phone_IsNull()
        {
            var validator = new CommonValidations();
            validator.PhoneValidation(null);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_When_Phone_IsMoreThan12()
        {
            var validator = new CommonValidations();

            validator.PhoneValidation("123456789123568911221");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_When_Phone_IsLessThan5()
        {
            var validator = new CommonValidations();

            validator.PhoneValidation("1");
        }
    }
}
