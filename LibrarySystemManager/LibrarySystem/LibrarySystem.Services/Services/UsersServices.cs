﻿using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Constants;
using LibrarySystem.Services.Exceptions.UserServices;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services
{
    public class UsersServices : BaseServicesClass, IUsersServices
    {
        public UsersServices(ILibrarySystemContext context) : base(context)
        {

        }

        public User AddUser(string firstName, string middleName, string lastName, int phoneNumber, DateTime addedOn, bool IsDeleted, Address address)
        {
            var query = context.Users
               .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (query != null)
            {
                throw new UserNullableExeption("User already exists.");
            }
            if (firstName.Length < ServicesConstants.MinUserNameLength
                || firstName.Length > ServicesConstants.MaxUserNameLength)
            {
                throw new ArgumentOutOfRangeException("First name should be between 1 and 20 symbols.");
            }
            if (middleName.Length < ServicesConstants.MinUserNameLength
                || middleName.Length > ServicesConstants.MaxUserNameLength)
            {
                throw new ArgumentOutOfRangeException("Middle name should be between 1 and 20 symbols.");
            }
            if (lastName.Length < ServicesConstants.MinUserNameLength
                || lastName.Length > ServicesConstants.MaxUserNameLength)
            {
                throw new ArgumentOutOfRangeException("Last name should be between 1 and 20 symbols.");
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

            user = this.context.Users.Add(user).Entity;
            this.context.SaveChanges();

            return user;
        }

        public User GetUser(string firstName, string middleName, string lastName)
        {
            var user = this.context.Users
                .Include(u => u.Address)
                    .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .SingleOrDefault(
                u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName
                );

            if (user == null || user.IsDeleted)
            {
                throw new UserNullableExeption("This user does not exists.");
            }
            return user;
        }

        public IEnumerable<User> ListUsers()
        {
            var users = this.context.Users
                .Include(u => u.Address)
                  .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .Where(u => !u.IsDeleted)
                .Take(10)
                .ToList();

            if (users.Count == 0)
            {
                throw new UserNullableExeption("No users were found.");
            }
            return users;
        }

        public User RemoveUser(string firstName, string middleName, string lastName)
        {
            var user = this.context.Users
                .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user == null)
            {
                throw new UserNullableExeption("This user does not exist.");
            }
            user.IsDeleted = true;
            this.context.SaveChanges();
            return user;
        }

        public User UpdateUserAddress(string firstName, string middleName, string lastName, Address address)
        {
            var user = this.context.Users
                .Include(u => u.Address)
                  .ThenInclude(a => a.Town)
                .SingleOrDefault(u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user == null || user.IsDeleted)
            {
                throw new UserNullableExeption("This user does not exist.");
            }

            user.Address.StreetAddress = address.StreetAddress;
            user.Address.Town = address.Town;

            this.context.SaveChanges();
            return user;
        }

        public User UpdateUserPhone(string firstName, string middleName, string lastName, int phone)
        {
            var user = this.context.Users
                .SingleOrDefault(
                u => u.FirstName == firstName
                && u.MiddleName == middleName
                && u.LastName == lastName);

            if (user == null || user.IsDeleted)
            {
                throw new UserNullableExeption("This user does not exist.");
            }
            user.PhoneNumber = phone;
            this.context.SaveChanges();

            return user;
        }
    }
}
