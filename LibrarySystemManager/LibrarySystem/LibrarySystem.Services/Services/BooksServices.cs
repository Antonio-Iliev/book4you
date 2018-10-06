using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services
{
    public class BooksServices : BaseServicesClass, IBooksServices
    {
        public BooksServices(ILibSysContext context) : base(context)
        {
        }

        public Book AddBook(string title, string genre, string author)
        {
            throw new NotImplementedException();
        }
    }
}
