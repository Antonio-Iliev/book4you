using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.UserServices
{
    public class InvalidUserServiceParametersExeption : Exception
    {
        public InvalidUserServiceParametersExeption(string message) : base(message)
        {
        }
    }
}
