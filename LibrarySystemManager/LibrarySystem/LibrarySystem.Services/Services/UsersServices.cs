using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Constants;

namespace LibrarySystem.Services
{
    public class UsersServices : BaseServicesClass, IUsersServices
    {
        public UsersServices(ILibrarySystemContext context) : base(context)
        {
        }

        // Address
        public User AddUser(string firstName, string middleName, string lastName, int phoneNumber, DateTime addedOn, bool IsDeleted, Address address)
        {
            var query = context.Users
               .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (query != null)
            {
                throw new ArgumentException("User already exists!");
            }

            if (firstName.Length < ServicesConstants.MinUserNameLength
                || firstName.Length > ServicesConstants.MaxUserNameLength)
            {
                throw new Exception();
            }
            if (middleName.Length < ServicesConstants.MinUserNameLength
                || middleName.Length > ServicesConstants.MaxUserNameLength)
            {
                throw new Exception();
            }
            if (lastName.Length < ServicesConstants.MinUserNameLength
                || lastName.Length > ServicesConstants.MaxUserNameLength)
            {
                throw new Exception();
            }
            var user = new User
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = address.Id
            };

            user = base.context.Users.Add(user).Entity;
            base.context.SaveChanges();

            return user;
        }

        public User GetUser(string firstName, string middleName, string lastName)
        {
            var user = this.context.Users
                .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user == null || user.IsDeleted == true)
            {
                throw new Exception();
            }

            //var result = this.context.Users
            //    .Select(u => new
            //    {
            //        FirstName = firstName,
            //        MiddleName = middleName,
            //        LastName = lastName,
            //        Phone = u.PhoneNumber,
            //        AddedOn = u.AddOnDate,
            //        IsDeleted = u.IsDeleted,
            //        Address = u.Address.StreetAddress + ' ' + u.Address.Town
            //    })
            //    .ToList();
            return user;
        }

        public IEnumerable<User> ListUsers()
        {
            if (this.context.Users.Count()==0)
            {
                throw new Exception("No users found");
            }
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

        public User UpdateUser(string firstName, string middleName, string lastName, Address address)
        {
            var result = this.context.Users
                .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (result != null)
            {
                result.Address.StreetAddress = address.StreetAddress;
                result.Address.Town = address.Town;

                this.context.SaveChanges();
            }
            return result;
        }
    }
}
