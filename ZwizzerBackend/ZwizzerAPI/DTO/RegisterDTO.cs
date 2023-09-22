using System.ComponentModel.DataAnnotations;

namespace ZwizzerAPI.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(1)]
        [MaxLength(32)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(320)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? BackgroundImagePath { get; set; }
        [MaxLength(160)]
        public string? Bio { get; set; }
    }
}
