using System;

namespace LibrarySystem.Data.Models
{
    public class UsersBooks
    {   
        public string UserId { get; set; }

        public Guid BookId { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }

        public bool IsReturn { get; set; }
    }
}
