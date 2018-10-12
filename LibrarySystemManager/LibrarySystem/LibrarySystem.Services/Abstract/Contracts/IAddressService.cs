using System.Collections.Generic;
using LibrarySystem.Data.Models;

namespace LibrarySystem.Services.Services
{
    public interface IAddressService
    {
        int AddAddress(string streetAddress, int town);
    }
}