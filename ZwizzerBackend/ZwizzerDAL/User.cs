using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwizzerDAL
{
    public enum UserRole
    {
        User = 0,
        Subscriber = 1,
        Admin = 2
    }
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(32)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(320)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        [Required]
        public UserRole UserRole { get; set; }

        public string ProfileImagePath { get; set; }
        public string BackgroundImagePath { get; set; }
        [MaxLength(160)]
        public string? Bio { get; set; }

        public ICollection<Zweet> Zweets { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Rezweet> Rezweets { get; set; }
    }
}
