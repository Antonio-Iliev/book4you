using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.WebClient.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required, MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
