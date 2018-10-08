using LibrarySystem.ConsoleClient.Commands.Constants;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.Data.Context;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.ConsoleClient.Commands
{
    public class AddUserCommand : ICommand
    {
        private readonly IUsersServices usersServices;
        private readonly IAddressService addressService;
        private readonly ITownService townService;

        public AddUserCommand(IUsersServices usersServices, IAddressService addressService, ITownService townService)
        {
            this.usersServices = usersServices;
            this.addressService = addressService;
            this.townService = townService;

        }

        //addUser firstName, middleName, lastName, int phoneNumber, DateTime addedOn, bool IsDeleted
        // Address & books TO DO
        public string Execute(IEnumerable<string> parameters)
        {
            var args = parameters.ToList();

            if (args.Count != 8)
            {
                throw new ArgumentException(CommandConstants.InvalidNumbersOfParameters);
            }
            var firstName = args[0];
            var middleName = args[1];
            var lastName = args[2];
            var phone = int.Parse(args[3]);
            var addedOn = DateTime.Now;
            bool isDeleted = false;
            Town town = townService.AddTown(args[5]);
            Address address = addressService.AddAddress(args[4], town);

            //Address address=new Address(args[4], )

            if (firstName.Length < CommandConstants.MinUserNameLength 
                || firstName.Length > CommandConstants.MaxUserNameLength)
            {
                return $"The first name {firstName} should be between " +
                    $"{CommandConstants.MinUserNameLength} and {CommandConstants.MaxUserNameLength} symbols.";
            }
            if (middleName.Length < CommandConstants.MinUserNameLength
                || middleName.Length > CommandConstants.MaxUserNameLength)
            {
                return $"The middle name {middleName} should be between " +
                    $"{CommandConstants.MinUserNameLength} and {CommandConstants.MaxUserNameLength} symbols.";
            }
            if (lastName.Length < CommandConstants.MinUserNameLength
                || lastName.Length > CommandConstants.MaxUserNameLength)
            {
                return $"The last name {lastName} should be between " +
                     $"{CommandConstants.MinUserNameLength} and {CommandConstants.MaxUserNameLength} symbols.";
            }
            
            usersServices.AddUser(firstName, middleName, lastName, phone, addedOn, isDeleted, address);

            return $"New user {firstName} {lastName} was added.";
        }
    }
}
