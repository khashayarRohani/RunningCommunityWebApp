using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = " Password confirming is required")]
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage ="Password is not the same")]
        public string ConfirmPassword { get; set; }
    }
}
