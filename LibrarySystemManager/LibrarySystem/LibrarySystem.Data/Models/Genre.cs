using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibrarySystem.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
