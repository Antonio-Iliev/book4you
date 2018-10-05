using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Data.Models
{
    public class UsersBooks
    {
        public Guid UserId { get; set; }

        public int BookId { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }
    }
}
