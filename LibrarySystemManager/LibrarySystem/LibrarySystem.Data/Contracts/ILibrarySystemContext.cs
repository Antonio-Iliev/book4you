using LibrarySystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Data.Contracts
{
    public interface ILibrarySystemContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Town> Towns { get; set; }
        DbSet<UsersAddresses> UsersAddresses { get; set; }
        DbSet<UsersBooks> UsersBooks { get; set; }

        int SaveChanges();
    }
}
