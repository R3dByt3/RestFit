using System.ComponentModel.DataAnnotations;

namespace RestFit.API.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
