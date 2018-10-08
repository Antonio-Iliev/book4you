using LibrarySystem.Data.Models;

namespace LibrarySystem.Services.Services
{
    public interface ITownService
    {
        Town AddTown(string townName);
    }
}