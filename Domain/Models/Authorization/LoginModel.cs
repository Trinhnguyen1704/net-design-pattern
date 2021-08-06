using System.ComponentModel.DataAnnotations;

namespace net_design_pattern.Domain.Models.Authorization
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}