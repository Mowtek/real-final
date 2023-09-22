using System.ComponentModel.DataAnnotations;

namespace ZwizzerAPI.DTO
{
    public class ZweetEditDTO
    {
        [Required]
        public int ZweetId { get; set; }
        [Required]
        [MaxLength(280)]
        public string Content { get; set; }
    }
}
