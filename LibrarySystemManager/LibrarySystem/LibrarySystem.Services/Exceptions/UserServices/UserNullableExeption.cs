using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.UserServices
{
    public class UserNullableExeption : Exception
    {
        public UserNullableExeption(string message) : base(message)
        {
        }
    }
}
