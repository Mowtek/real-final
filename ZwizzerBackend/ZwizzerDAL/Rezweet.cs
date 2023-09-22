using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwizzerDAL
{
    public class Rezweet
    {
        [Key]
        public int RezweetId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int ZweetId { get; set; }
        public Zweet Zweet { get; set; }
    }
}
