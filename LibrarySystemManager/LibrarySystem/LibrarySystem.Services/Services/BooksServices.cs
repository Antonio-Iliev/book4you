using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Services
{
    public class BooksServices : BaseServicesClass, IBooksServices
    {
        public BooksServices(ILibrarySystemContext context) : base(context)
        {
        }

        public Book AddBook(string title, string genre, string author)
        {
            throw new NotImplementedException();
        }
    }
}
