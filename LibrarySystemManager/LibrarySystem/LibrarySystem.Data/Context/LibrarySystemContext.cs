using LibrarySystem.Data.Models;
using Microsoft.AspNetCore.Hosting;
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

namespace LibrarySystem.Data.Context
{
    public class LibrarySystemContext : IdentityDbContext<User>
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private string webRoot; 

        public LibrarySystemContext
            (DbContextOptions<LibrarySystemContext> options, IHostingEnvironment hostingEnvironment)
            : base(options)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.webRoot = this.hostingEnvironment.WebRootPath;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<UsersBooks> UsersBooks { get; set; }
        public DbSet<UsersReadBooks> UsersReadBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=LibrarySystem;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var authorsNames = ReadLinesTextFile("AuthorNames.txt");
            GetApiData(authorsNames);

            var genres = JsonConvert.DeserializeObject<Genre[]>(ReadJsonFile("Genres.json"));
            var towns = JsonConvert.DeserializeObject<Town[]>(ReadJsonFile("Towns.json"));
            var authors = JsonConvert.DeserializeObject<Author[]>(ReadJsonFile("Authors.json"));
            var books = JsonConvert.DeserializeObject<Book[]>(ReadJsonFile("Books.json"));

            modelBuilder.Entity<Town>().HasData(towns);
            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Book>().HasData(books);

            SeedAdminUser(modelBuilder);

            SetPrimeryKeys(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SetPrimeryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersBooks>()
                .HasKey(p => new { p.UserId, p.BookId });

            modelBuilder.Entity<UsersReadBooks>()
                .HasKey(p => new { p.UserId, p.BookId });
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

        private void GetApiData(string[] authorsNames)
        {
            List<Author> authors = JsonConvert.DeserializeObject<List<Author>>(ReadJsonFile("Authors.json"));
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(ReadJsonFile("Books.json"));

            var client = new WebClient();
            foreach (var authorName in authorsNames)
            {
                string name = authorName.Replace(" ", "%20");
                string response = client.DownloadString("https://api.nytimes.com/svc/books/v3/lists/best-sellers/history.json?api-key=f8e19acc6ac940daa7ae7456e943da68&author=" + name);

                JObject json = JObject.Parse(response);
                foreach (var item in json["results"])
                {
                    string itemAuthorName = item["author"]
                        .ToString()
                        .Split(new string[] { ",", "and" }, StringSplitOptions.RemoveEmptyEntries).First();
                    Author author = authors.FirstOrDefault(a => a.Name.ToLower() == itemAuthorName.ToLower());

                    if (author == null)
                    {
                        author = new Author()
                        {
                            Name = itemAuthorName,
                            Id = authors.Count + 1
                        };

                        authors.Add(author);
                    }

                    Book book = new Book();
                    book.Title = item["title"].ToString().Length < 500 ? item["title"].ToString() : item["title"].ToString().Substring(0, 50);
                    book.Description = item["description"].ToString().Length < 500 ? item["description"].ToString() : string.Empty;
                    book.BooksInStore = 10;
                    book.GenreId = new Random().Next(1, 10);
                    book.AuthorId = author.Id;
                    if (books.Count(b => b.Title.ToLower() == book.Title.ToLower()) == 0)
                    {
                        book.Id = Guid.NewGuid();
                        books.Add(book);
                    }
                }
            }

            string jsonAuthors = JsonConvert.SerializeObject(authors);
            string jsonBooks = JsonConvert.SerializeObject(books);

            WriteJsonFile("Authors.json", jsonAuthors);
            WriteJsonFile("Books.json", jsonBooks);
        }


        private string ReadJsonFile(string fileName)
        {
            return File.ReadAllText(this.webRoot + "/Database-jason/" + fileName);
        }

        private void WriteJsonFile(string fileName, string data)
        {
            File.WriteAllText(this.webRoot + "/Database-jason/" + fileName, data);
        }

        private string[] ReadLinesTextFile(string fileName)
        {
            return File.ReadAllLines(this.webRoot + "/Database-jason/" + fileName);
        }
    }
}
