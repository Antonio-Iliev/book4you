using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;

namespace LibrarySystem.Services
{
    public class UsersServices : BaseServicesClass, IUsersServices
    {
        public UsersServices(ILibrarySystemContext context) : base(context)
        {
        }

        // Address
        public User AddUser(string firstName, string middleName, string lastName, int phoneNumber, DateTime addedOn, bool IsDeleted)
        {
            var user = new User
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                AddOnDate = DateTime.Now,
                IsDeleted = false
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public User GetUser(string firstName, string middleName, string lastName)
        {
            var user = this.context.Users.
            SingleOrDefault(p => p.FirstName == firstName
            && p.MiddleName == middleName
            && p.LastName == lastName);

            return user;
        }

        public IEnumerable<User>ListUsers()
        {
            return this.context.Users.ToList();
        }

        public User RemoveUser(string firstName, string middleName, string lastName)
        {
            var result = this.context.Users
                .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (result != null)
            {
                result.IsDeleted = true;
                this.context.SaveChanges();
            }
            return result;
        }

        public User UpdateUser(string firstName, string middleName, string lastName, int phoneNumber, DateTime addedOn, bool IsDeleted, ICollection<UsersBooks> books)
        {
            throw new NotImplementedException();
        }
    }
}
