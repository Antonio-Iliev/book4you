using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.BookServices
{
    public class AddBookNullableExeption : Exception
    {
        public AddBookNullableExeption(string message) : base(message)
        {
        }
    }
}
