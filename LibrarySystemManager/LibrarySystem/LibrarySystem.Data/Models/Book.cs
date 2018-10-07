using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Data.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }
        
        public bool IsAvailable { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        
        public virtual ICollection<UsersBooks> UsersBooks { get; set; }
    }
}
