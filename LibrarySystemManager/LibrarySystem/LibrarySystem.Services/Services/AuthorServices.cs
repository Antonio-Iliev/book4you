using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using System.Linq;

namespace LibrarySystem.Services.Services
{
    public class AuthorServices : BaseServicesClass, IAuthorServices
    {
        public AuthorServices(UnitOfWork unitOfWork, IValidations validations) : base(unitOfWork, validations)
        {
        }

        public int AddAuthor(string authorName)
        {
            this.validations.AuthorValidation(authorName);

            Author newAuthor = this.unitOfWork.GetRepo<Author>().All().FirstOrDefault(a => a.Name == authorName);

            if (newAuthor == null)
            {
                this.unitOfWork.GetRepo<Author>().Add(new Author { Name = authorName });
                this.unitOfWork.SaveChanges();
                newAuthor = this.unitOfWork.GetRepo<Author>().All().FirstOrDefault(a => a.Name == authorName);
            }

            return newAuthor.Id;
        }

        public Author GetAuthor(string authorName)
        {
            this.validations.AuthorValidation(authorName);

            return this.unitOfWork.GetRepo<Author>().All().FirstOrDefault(a => a.Name == authorName);
        }
    }
}
