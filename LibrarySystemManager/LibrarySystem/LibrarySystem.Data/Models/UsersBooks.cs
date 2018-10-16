﻿using System;

namespace LibrarySystem.Data.Models
{
    public class UsersBooks
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }
    }
}
