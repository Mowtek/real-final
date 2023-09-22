using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwizzerDAL
{
    public class Zweet
    {
        [Key]
        public int ZweetId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [MaxLength(280)]
        public string Content { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }

        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rezweet> Rezweets { get; set; }
    }
}
