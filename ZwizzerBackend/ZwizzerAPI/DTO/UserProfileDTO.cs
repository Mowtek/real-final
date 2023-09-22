using System.ComponentModel.DataAnnotations;
using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class UserProfileDTO
    {
        public UserProfileDTO() {}

        public UserProfileDTO(User user)
        {
            UserName = user.UserName;
            UserRole = user.UserRole;
            UserId = user.UserId;
            Email = user.Email;
            ProfileImagePath = user.ProfileImagePath;
            BackgroundImagePath = user.BackgroundImagePath;
            Bio = user.Bio;
            Zweets = user.Zweets.Select(z => new ZweetDTO(z)).OrderByDescending(z=> z.CreationTime).ToList();
            Comments = user.Comments.Select(c => { var comment = new CommentDTO(c); comment.Zweet = new ZweetDTO(c.Zweet); return comment; }).OrderByDescending(c => c.CreationTime).ToList();
            Likes = user.Likes.Select(l => new LikeDTO(l)).OrderByDescending(l => l.CreationTime).ToList();
            Rezweets = user.Rezweets.Select(r => new RezweetDTO(r)).OrderByDescending(r => r.CreationTime).ToList();
        }

        public string UserName { get; set; }
        public UserRole UserRole { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string ProfileImagePath { get; set; }
        public string BackgroundImagePath { get; set; }
        public string Bio { get; set; }
        public ICollection<ZweetDTO> Zweets { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<LikeDTO> Likes { get; set; }
        public ICollection<RezweetDTO> Rezweets { get; set; }
    }
}
