using System.Collections.Generic;

namespace LibrarySystem.WebClient.Areas.Administration.Models
{
    public class ListUsersModel
    {

        public ListUsersModel(IEnumerable<UserViewModel> users, int currentPage)
        {
            this.Users = users;
            this.CurrentPage = currentPage;
        }

        public IEnumerable<UserViewModel> Users { get; set; }

        public int CurrentPage { get; set; }
    }
}
