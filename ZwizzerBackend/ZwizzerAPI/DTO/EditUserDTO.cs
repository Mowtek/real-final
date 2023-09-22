using System.ComponentModel.DataAnnotations;
using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class EditUserDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(320)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
        [MaxLength(160)]
        public string Bio { get; set; }
    }
}
