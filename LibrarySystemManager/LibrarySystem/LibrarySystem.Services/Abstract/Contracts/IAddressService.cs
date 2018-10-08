using System.Collections.Generic;
using LibrarySystem.Data.Models;

namespace LibrarySystem.Services.Services
{
    public interface IAddressService
    {
        Address AddAddress(string streetAddress, Town town);
        IEnumerable<Address> GetAddress(string streetAddress);
    }
}