using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Data.Models
{
    public class Address
    {
        [Key]
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