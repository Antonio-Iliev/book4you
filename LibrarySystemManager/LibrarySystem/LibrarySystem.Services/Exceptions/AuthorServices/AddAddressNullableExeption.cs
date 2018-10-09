using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.AuthorServices
{
    public class AddAuthorNullableExeption : Exception
    {
        public AddAuthorNullableExeption(string message) : base(message)
        {
        }
    }
}
