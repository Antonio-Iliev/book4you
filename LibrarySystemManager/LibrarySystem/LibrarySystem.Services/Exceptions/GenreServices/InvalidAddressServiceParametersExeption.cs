using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.GenreServiceExeptions
{
    public class InvalidGenreServiceParametersExeption : Exception
    {
        public InvalidGenreServiceParametersExeption(string message) : base(message)
        {
        }
    }
}
