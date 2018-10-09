using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.AddressServices
{
    public class InvalidAddressServiceParametersExeption : Exception
    {
        public InvalidAddressServiceParametersExeption(string message) : base(message)
        {
        }
    }
}
