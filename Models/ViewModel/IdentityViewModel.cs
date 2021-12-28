using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UncleApp.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [StringLength(120,MinimumLength = 6,ErrorMessage ="Your string is less than 6 chars")]
        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        public bool RememberMe { get; set; } = false;
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [StringLength(120, MinimumLength = 6, ErrorMessage = "Your string is less than 6 chars")]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [StringLength(120, MinimumLength = 6, ErrorMessage = "Your string is less than 6 chars")]
        [Required]
        [Compare("Password",ErrorMessage ="Your Password is not same")]
        public string ConPassword { get; set; }
    }
}
