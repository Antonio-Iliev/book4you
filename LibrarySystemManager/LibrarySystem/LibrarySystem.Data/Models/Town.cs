using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Data.Models
{
    public class Town
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string TownName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}