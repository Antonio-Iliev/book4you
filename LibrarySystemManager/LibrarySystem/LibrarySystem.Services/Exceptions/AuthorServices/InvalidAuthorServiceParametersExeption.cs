using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.AuthorServices
{
    public class InvalidAuthorServiceParametersExeption : Exception
    {
        public InvalidAuthorServiceParametersExeption(string message) : base(message)
        {
        }
    }
}
