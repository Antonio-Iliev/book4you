using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.AuthorServices;
using LibrarySystem.Services.ViewModels;
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

        public int AddAuthor(string authorName)
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

            return newAuthor.Id;
        }

        public AuthorViewModel GetAuthor(string authorName)
        {
            this.validations.AuthorValidation(authorName);

            var searchAuthor = this.context.Authors
                .Include(b => b.Books).ToList()
                .FirstOrDefault(a => a.Name == authorName);

            if (searchAuthor == null)
            {
                throw new AddAuthorNullableExeption("There is no such author in this Library.");
            }

            AuthorViewModel author = new AuthorViewModel()
            {
                AuthorName = searchAuthor.Name,
                AuthorBooks = searchAuthor.Books
            };

            return author;
        }
    }
}
