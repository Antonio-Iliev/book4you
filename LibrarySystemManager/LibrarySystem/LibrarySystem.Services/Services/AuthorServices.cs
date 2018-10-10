using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using System.Linq;

namespace LibrarySystem.Services.Services
{
    public class AuthorServices : BaseServicesClass, IAuthorServices
    {
        public AuthorServices(ILibrarySystemContext context, IValidations validations) : base(context, validations)
        {
        }

        public Author AddAuthor(string authorName)
        {
            this.validations.AuthorValidation(authorName);

            Author newAuthor = context.Authors.FirstOrDefault(a => a.Name == authorName);

            if (newAuthor == null)
            {
                newAuthor = this.context.Authors.Add(new Author { Name = authorName }).Entity;
                this.context.SaveChanges();
            }

            return newAuthor;
        }

        public Author GetAuthor(string authorName)
        {
            this.validations.AuthorValidation(authorName);

            return context.Authors.FirstOrDefault(a => a.Name == authorName);
        }
    }
}
