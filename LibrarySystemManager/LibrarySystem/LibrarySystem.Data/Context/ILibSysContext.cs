using LibrarySystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Context
{
    public interface ILibSysContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Town> Towns { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UsersAddresses> UsersAddresses { get; set; }
        DbSet<UsersBooks> UsersBooks { get; set; }

        int SaveChanges();
    }
}