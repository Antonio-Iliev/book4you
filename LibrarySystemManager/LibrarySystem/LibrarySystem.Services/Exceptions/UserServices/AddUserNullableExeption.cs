using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.UserServices
{
    public class AddUserNullableExeption : Exception
    {
        public AddUserNullableExeption(string message) : base(message)
        {
        }
    }
}
