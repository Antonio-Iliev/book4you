using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.TownServiceExeptions
{
    public class InvalidTownServiceParametersExeption : Exception
    {
        public InvalidTownServiceParametersExeption(string message) : base(message)
        {
        }
    }
}
