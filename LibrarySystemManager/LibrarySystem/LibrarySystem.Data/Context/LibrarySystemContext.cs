using LibrarySystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace LibrarySystem.Data.Context
{
    public class LibrarySystemContext : IdentityDbContext<User>
    {
        public LibrarySystemContext(DbContextOptions<LibrarySystemContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Town> Towns { get; set; }
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
            var genres = JsonConvert.DeserializeObject<Genre[]>(ReadJsonFile("Genres.json"));
            var authors = JsonConvert.DeserializeObject<Author[]>(ReadJsonFile("Authors.json"));
            var books = JsonConvert.DeserializeObject<Book[]>(ReadJsonFile("Books.json"));
            var towns = JsonConvert.DeserializeObject<Town[]>(ReadJsonFile("Towns.json"));
            
            modelBuilder.Entity<Town>().HasData(towns);
            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Book>().HasData(books);
            
            modelBuilder.Entity<UsersBooks>()
                .HasKey(p => new { p.UserId, p.BookId });


            modelBuilder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "Admin", Id = 1.ToString(), NormalizedName = "Admin".ToUpper() });

            modelBuilder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "User", Id = 2.ToString(), NormalizedName = "User".ToUpper() });

            modelBuilder.Entity<Address>().HasData(new Address
            { Id = 1, StreetAddress = "AdminAddres", TownId = 1 });

            var adminUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Admin",
                LastName = "AdminLastName",
                UserName = "adminMain",
                NormalizedUserName = "adminMain".ToUpper(),
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+111111111",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                AddressId = 1
            };

            var hashePass = new PasswordHasher<User>().HashPassword(adminUser, "magicString");
            adminUser.PasswordHash = hashePass;

            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = 1.ToString(),
                UserId = adminUser.Id
            });

            base.OnModelCreating(modelBuilder);
        }

        private string ReadJsonFile(string fileName)
        {
            if (File.Exists("../LibrarySystem.Data/Files/" + fileName))
            {
                return File.ReadAllText("../LibrarySystem.Data/Files/" + fileName);
            }
            else
            {
                return File.ReadAllText("../../../../LibrarySystem.Data/Files/" + fileName);
            }
        }
    }
}
