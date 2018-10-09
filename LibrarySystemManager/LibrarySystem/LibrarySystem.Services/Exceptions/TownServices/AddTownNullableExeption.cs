using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services.Exceptions.TownServices
{
    public class AddTownNullableExeption : Exception
    {
        public AddTownNullableExeption(string message) : base(message)
        {
        }
    }
}
