using System;

namespace LibrarySystem.Services.Exceptions.UserServices
{
    public class UserNullableException : Exception
    {
        public UserNullableException(string message) : base(message)
        {
        }
    }
}
