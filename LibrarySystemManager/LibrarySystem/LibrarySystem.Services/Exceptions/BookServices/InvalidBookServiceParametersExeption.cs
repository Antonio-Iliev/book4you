using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.BookServiceExeptions
{
    public class InvalidBookServiceParametersExeption : Exception
    {
        public InvalidBookServiceParametersExeption(string message) : base(message)
        {
        }
    }
}
