using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Data.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string StreetAddress { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}