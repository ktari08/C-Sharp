using System.ComponentModel.DataAnnotations;

namespace bright_ideas.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string loginEmail {get;set;}

        [Required(ErrorMessage = "Enter a password")]
        [MinLength(8, ErrorMessage = "Must be at least 8 characters")]
        public string loginPassword {get;set;}
    }
}