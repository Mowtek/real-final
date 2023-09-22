using System.ComponentModel.DataAnnotations;

namespace ZwizzerAPI.DTO
{
    public class NewCommentDTO
    {
        [Required]
        public int ZweetId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(280)]
        public string Content { get; set; }
    }
}
