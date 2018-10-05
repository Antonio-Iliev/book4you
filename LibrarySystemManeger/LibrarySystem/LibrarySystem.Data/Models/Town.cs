using System.Collections.Generic;

namespace LibrarySystem.Data.Models
{
    public class Town
    {
        public int Id { get; set; }
        public string TownName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}