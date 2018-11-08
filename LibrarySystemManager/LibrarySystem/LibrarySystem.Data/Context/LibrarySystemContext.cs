using LibrarySystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            var towns = JsonConvert.DeserializeObject<Town[]>(ReadJsonFile("Towns.json"));
            var authorsNames = ReadLinesTextFile("AuthorNames.json");

            modelBuilder.Entity<Town>().HasData(towns);
            modelBuilder.Entity<Genre>().HasData(genres);

            SeedApiData(modelBuilder, authorsNames);
            SeedAdminUser(modelBuilder);

            modelBuilder.Entity<UsersBooks>()
                .HasKey(p => new { p.UserId, p.BookId });

            base.OnModelCreating(modelBuilder);
        }

        private void SeedAdminUser(ModelBuilder modelBuilder)
        {
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
        }

        private void SeedAuthors(ModelBuilder modelBuilder, Author[] authors)
        {
            modelBuilder.Entity<Author>().HasData(authors);
        }

        private void SeedBooks(ModelBuilder modelBuilder, Book[] books)
        {
            modelBuilder.Entity<Book>().HasData(books);
        }

        private void SeedApiData(ModelBuilder modelBuilder, string[] authorsNames)
        {
            var books = new List<Book>();
            var authors = new List<Author>();

            var client = new WebClient();
            foreach (var authorName in authorsNames)
            {
                HashSet<int> ids = new HashSet<int>();
                int authorId = 0;
                string name = authorName.Replace(" ", "%20");
                string response = client.DownloadString("https://api.nytimes.com/svc/books/v3/lists/best-sellers/history.json?api-key=f8e19acc6ac940daa7ae7456e943da68&author=" + name);

                JObject json = JObject.Parse(response);
                foreach (var item in json["results"])
                {
                    Author author = new Author();
                    author.Name = item["author"].ToString();
                    author.Id = authorId;

                    Book book = new Book();
                    book.Id = Guid.NewGuid();
                    book.Title = item["title"].ToString().Length < 500 ? item["title"].ToString() : item["title"].ToString().Substring(0, 50);
                    book.Description = item["description"].ToString().Length < 500 ? item["description"].ToString() : string.Empty;
                    book.BooksInStore = 10;
                    book.GenreId = new Random().Next(1, 10);
                    book.AuthorId = author.Id;

                    if (!ids.Contains(author.Id))
                    {
                        authors.Add(author);
                        ids.Add(author.Id);
                    }

                    books.Add(book);
                    authorId++;
                }
            }

            SeedAuthors(modelBuilder, authors.ToArray());
            SeedBooks(modelBuilder, books.ToArray());
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

        private string[] ReadLinesTextFile(string fileName)
        {
            if (File.Exists("../LibrarySystem.Data/Files/" + fileName))
            {
                return File.ReadAllLines("../LibrarySystem.Data/Files/" + fileName);
            }
            else
            {
                return File.ReadAllLines("../../../../LibrarySystem.Data/Files/" + fileName);
            }
        }
    }
}
