using System.ComponentModel.DataAnnotations;

namespace wedding_planner.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string email {get;set;}

        [Required(ErrorMessage = "Enter a password")]
        [MinLength(8, ErrorMessage = "Must be at least 8 characters")]
        public string password {get;set;}
    }
}