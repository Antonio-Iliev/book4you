using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.AuthorServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibrarySystem.Services.Services
{
    public class AuthorServices : BaseServicesClass, IAuthorServices
    {
        public AuthorServices(ILibrarySystemContext context, IValidations validations)
            : base(context, validations)
        {
        }

        public Author AddAuthor(string authorName)
        {
            this.validations.AuthorValidation(authorName);

            Author newAuthor = this.context.Authors
                .FirstOrDefault(a => a.Name == authorName);

            if (newAuthor == null)
            {
                this.context.Authors.Add(new Author { Name = authorName });
                this.context.SaveChanges();
                newAuthor = this.context.Authors.FirstOrDefault(a => a.Name == authorName);
            }

            return newAuthor;
        }

        public Author GetAuthor(string authorName)
        {
            this.validations.AuthorValidation(authorName);

            var author = this.context.Authors
                .Include(b => b.Books).ToList()
                .FirstOrDefault(a => a.Name == authorName);

            if (author == null)
            {
                throw new AddAuthorNullableExeption("There is no such author in this Library.");
            }

            return author;
        }
    }
}
