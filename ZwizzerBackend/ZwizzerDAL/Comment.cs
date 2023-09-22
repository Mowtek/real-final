using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwizzerDAL
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int ZweetId { get; set; }
        public Zweet Zweet { get; set; }
        [Required]
        [MaxLength(280)]
        public string Content { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
