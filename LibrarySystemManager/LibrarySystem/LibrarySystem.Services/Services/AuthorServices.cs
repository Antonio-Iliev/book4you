using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Services.Services
{
    public class AuthorServices : BaseServicesClass, IAuthorServices
    {
        public AuthorServices(ILibrarySystemContext context) : base(context)
        {
        }

        public Author AddAuthor(string authorName)
        {
            Author newAuthor = context.Authors.FirstOrDefault(a => a.Name == authorName);

            if (newAuthor == null)
            {
                newAuthor = new Author
                {
                    Name = authorName
                };
                context.Authors.Add(newAuthor);
            }

            return newAuthor;
        }

        public Author GetAuthor(string authorName)
        {
            return context.Authors.FirstOrDefault(a => a.Name == authorName);
        }
    }
}
