using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.GenreServices
{
    public class AddGenreNullableExeption : Exception
    {
        public AddGenreNullableExeption(string message) : base(message)
        {
        }
    }
}
