using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibrarySystem.Data.Context
{
    public class LibrarySystemContext : DbContext, ILibrarySystemContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<UsersAddresses> UsersAddresses { get; set; }
        public DbSet<UsersBooks> UsersBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=LibrarySystem;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var genres = JsonConvert.DeserializeObject<Genre[]>(File.ReadAllText("../LibrarySystem.Data/Files/Genres.json"));
            var authors = JsonConvert.DeserializeObject<Author[]>(File.ReadAllText("../LibrarySystem.Data/Files/Authors.json"));
            var books = JsonConvert.DeserializeObject<Book[]>(File.ReadAllText("../LibrarySystem.Data/Files/Books.json"));
            var towns = JsonConvert.DeserializeObject<Town[]>(File.ReadAllText("../LibrarySystem.Data/Files/Towns.json"));

            modelBuilder.Entity<Town>().HasData(towns);
            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Book>().HasData(books);

            modelBuilder.Entity<UsersAddresses>()
                    .HasKey(p => new { p.UserId, p.AddressId });

            modelBuilder.Entity<UsersBooks>()
                .HasKey(p => new { p.UserId, p.BookId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
