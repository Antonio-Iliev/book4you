using LibrarySystem.Services.Exceptions.TownServices;
using LibrarySystem.Services.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Services.ValidationsTests
{
    public class TownValidation_Should
    {
        [TestMethod]
        [ExpectedException(typeof(AddTownNullableExeption))]
        public void Throw_When_TownName_IsNull()
        {
            var validator = new CommonValidations();

            validator.TownValidation(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTownServiceParametersExeption))]
        public void Throw_When_TownName_IsMoreThan50()
        {
            var validator = new CommonValidations();

            validator.TownValidation(new string('a', 51));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTownServiceParametersExeption))]
        public void Throw_When_TownName_IsEmpty()
        {
            var validator = new CommonValidations();

            validator.TownValidation("");
        }
    }
}
