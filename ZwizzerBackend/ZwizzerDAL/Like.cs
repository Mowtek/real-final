using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwizzerDAL
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public int? ZweetId { get; set; }
        public Zweet Zweet { get; set; }
        public int? CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
