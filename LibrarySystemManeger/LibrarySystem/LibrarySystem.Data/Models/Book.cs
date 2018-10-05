using System;
using System.Collections.Generic;

namespace LibrarySystem.Data.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public bool IsAvailable { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        
        public virtual ICollection<UsersBooks> UsersBooks { get; set; }
    }
}
