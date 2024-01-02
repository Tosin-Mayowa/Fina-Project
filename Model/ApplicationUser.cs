using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class ApplicationUser:IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string Role { get; set; }
        public string Dept { get; set; }
    }
}
