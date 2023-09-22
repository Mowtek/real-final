using System.ComponentModel.DataAnnotations;

namespace ZwizzerAPI.DTO
{
    public class ZweetCreationDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(280)]
        public string Content { get; set; }
    }
}
