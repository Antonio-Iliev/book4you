using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Data.Models
{
    public class Author
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
