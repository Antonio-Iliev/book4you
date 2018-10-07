using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;

namespace LibrarySystem.Services
{
    public class UsersServices : IUsersServices
    {
        private ILibrarySystemContext libraryContext;

        public UsersServices(ILibrarySystemContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }

        // Address
        public User AddUser(string firstName, string middleName, string lastName, int phoneNumber, DateTime addedOn, bool IsDeleted)
        {
            var user = new User
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                PhoneNumber=phoneNumber,
                AddOnDate = DateTime.Now,
                IsDeleted = false
            };

            this.libraryContext.Users.Add(user);
            this.libraryContext.SaveChanges();

            return user;
        }
       
        public IEnumerable<User> ListUsers(string firstName, string middleName, string lastName)
        {
            var usersQuery = this.libraryContext.Users.AsQueryable();

            if (firstName != null && middleName!=null && lastName != null)
            {
                usersQuery = usersQuery.Where(p => p.FirstName == firstName 
                && p.MiddleName==middleName
                && p.LastName == lastName);
            }
            return usersQuery.ToList();
        }

        public User RemoveUser(string firstName, string middleName, string lastName)
        {
            var result = this.libraryContext.Users
                .SingleOrDefault(u => u.FirstName == firstName 
                && u.MiddleName==middleName 
                && u.LastName == lastName);

            if (result != null)
            {
                result.IsDeleted = true;
                this.libraryContext.SaveChanges();
            }
            return result;
        }

        public User UpdateUser(string firstName, string middleName, string lastName, int phoneNumber, DateTime addedOn, bool IsDeleted, ICollection<UsersBooks> books)
        {
            throw new NotImplementedException();
        }
    }
}
