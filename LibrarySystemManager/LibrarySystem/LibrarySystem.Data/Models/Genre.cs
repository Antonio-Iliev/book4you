using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
