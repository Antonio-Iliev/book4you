using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.AddressServices
{
    public class AddAddressNullableExeption : Exception
    {
        public AddAddressNullableExeption(string message) : base(message)
        {
        }
    }
}
