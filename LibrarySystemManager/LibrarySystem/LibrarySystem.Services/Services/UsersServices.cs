using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Constants.Enumeration;
using LibrarySystem.Services.Exceptions.BookServices;
using LibrarySystem.Services.Exceptions.UserServices;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services
{
    public class UsersServices : BaseServicesClass, IUsersServices
    {
        public UsersServices(LibrarySystemContext context, IValidations validations)
            : base(context, validations)
        {
        }

        public IEnumerable<User> ListUsers(string listUsersBy, int pageSize, int page)
        {
            if (!Enum.TryParse(listUsersBy.ToLower(), out ListUsersCategory filter))
            {
                throw new InvalidUserServiceParametersExeption("Invalid input parameters");
            }

            IQueryable<User> user = this.context.Users
                .Include(u => u.Address)
                .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                .ThenInclude(ub => ub.Book);

            switch (filter)
            {
                case ListUsersCategory.deleted:
                    user = user.Where(u => u.IsDeleted == true);
                    break;
                case ListUsersCategory.active:
                    user = user.Where(u => u.IsDeleted == false);
                    break;
            }

            if (user.Count() == 0)
            {
                throw new UserNullableException("No users were found.");
            }

            user = user
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return user;
        }

        public User BorrowBook(string userId, Guid bookId)
        {
            var user = this.context.Users
                  .SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }

            var bookForBorrow = this.context.Books
                .SingleOrDefault(b => b.Id == bookId);

            if (bookForBorrow == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library");
            }
            if (bookForBorrow.BooksInStore - 1 < 0)
            {
                throw new AddBookNullableExeption("There is no enough books in store");
            }

            var isBorrow = this.context.UsersBooks
                .Select(b => b)
                .Where(b => b.BookId == bookForBorrow.Id && b.UserId == user.Id)
                .ToList();

            if (isBorrow.Count != 0)
            {
                throw new AddBookNullableExeption($"User {user.FirstName} already borrow this book '{bookForBorrow.Title}'.");
            }

            var usersBooks = new UsersBooks
            {
                UserId = user.Id,
                BookId = bookForBorrow.Id,
                BorrowDate = DateTime.Now
            };

            user.UsersBooks.Add(usersBooks);
            bookForBorrow.BooksInStore--;
            this.context.SaveChanges();

            return user;
        }

        public Book ReturnBook(string userId, Guid bookId)
        {
            var bookToReturn = this.context.Books
                    .SingleOrDefault(b => b.Id == bookId);

            if (bookToReturn == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library");
            }

            var userBook = this.context.UsersBooks
                .SingleOrDefault(ub => ub.UserId == userId && ub.BookId == bookId);

            if (userBook == null)
            {
                throw new UserNullableException("This user didn't borrow this book.");
            }

            var readBook = this.context.UsersReadBooks
                .SingleOrDefault(rb => rb.UserId == userId && rb.BookId == bookId);

            if (readBook != null)
            {
                readBook.BackDate = DateTime.Now;
                this.context.UsersBooks.Remove(userBook);
            }
            else
            {
                var returnedBook = new UsersReadBooks()
                {
                    UserId = userBook.UserId,
                    BookId = userBook.BookId,
                    BackDate = DateTime.Now
                };

                this.context.UsersReadBooks.Add(returnedBook);
                this.context.UsersBooks.Remove(userBook);
                bookToReturn.BooksInStore++;
            }

            this.context.SaveChanges();

            return bookToReturn;
        }

        public User GetUserById(string id)
        {
            var user = this.context.Users
                .Include(u => u.Address).ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks).ThenInclude(ub => ub.Book)
                .Include(u => u.UsersReadBooks).ThenInclude(rb => rb.Book)
                .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }
            return user;
        }
        public User RemoveUserById(string id)
        {
            var user = this.context.Users
                .SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }

            user.IsDeleted = true;
            this.context.SaveChanges();

            return user;
        }
        public User RestoreUserById(string id)
        {
            var user = this.context.Users
                .SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }

            user.IsDeleted = false;
            this.context.SaveChanges();

            return user;
        }

        public User BorrowBook(string id, string bookTitle)
        {
            this.validations.BookTitleValidation(bookTitle);

            var user = this.context.Users
                .SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }
            var bookForBorrow = this.context.Books
                .Include(a => a.Author)
                .Include(g => g.Genre)
                .FirstOrDefault(b => b.Title == bookTitle);

            if (bookForBorrow == null)
            {
                throw new AddBookNullableExeption("There is no such book in this Library");
            }
            if (bookForBorrow.BooksInStore - 1 < 0)
            {
                throw new AddBookNullableExeption("There is no enough books in store");
            }

            var isBorrow = this.context.UsersBooks
                .Select(b => b)
                .Where(b => b.BookId == bookForBorrow.Id && b.UserId == user.Id).ToList();

            if (isBorrow.Count != 0)
            {
                throw new AddBookNullableExeption($"User {user.FirstName} already borrow this book '{bookTitle}'.");
            }

            bookForBorrow.BooksInStore--;

            var usersBooks = new UsersBooks
            {
                User = user,
                Book = bookForBorrow
            };

            user.UsersBooks.Add(usersBooks);
            this.context.SaveChanges();

            return user;
        }

        public User ReturnBook(string id, string bookTitle)
        {
            this.validations.BookTitleValidation(bookTitle);

            var user = GetUserById(id);
            if (user == null)
            {
                throw new UserNullableException("There is no such user in this Library.");
            }
            var book = user.UsersBooks.SingleOrDefault(b => b.Book.Title == bookTitle);
            if (book == null)
            {
                throw new UserNullableException("This user never borrowed this book.");
            }
            var bookId = book.BookId;
            var bookInStore = this.context.Books.Find(bookId);
            bookInStore.BooksInStore++;

            user.UsersBooks.Remove(book);

            this.context.SaveChanges();

            return user;
        }

        public User UpdateUser(string id, string firstName, string middleName, string lastName,
            string phone, Address address)
        {
            var currentUser = this.context.Users
                .Include(u => u.Address)
                    .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .SingleOrDefault(u => u.Id == id);

            if (currentUser == null)
            {
                throw new UserNullableException("This user does not exist.");
            }
            currentUser.FirstName = firstName;
            currentUser.MiddleName = middleName;
            currentUser.LastName = lastName;
            currentUser.PhoneNumber = phone;
            currentUser.Address = address;
            currentUser.AddressId = address.Id;

            this.context.SaveChanges();
            return currentUser;
        }
    }
}
