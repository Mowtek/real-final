using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class CommentDTO
    {
        public CommentDTO() { }

        public CommentDTO(Comment comment)
        {
            if (comment != null && comment.Zweet != null)
            {
                ZweetId = comment.Zweet.ZweetId;
                UserId = comment.UserId;
                CommentId = comment.CommentId;
                if (comment.User != null)
                {
                    UserPhotoPath = comment.User.ProfileImagePath;
                    UserName = comment.User.UserName;
                }
                Content = comment.Content;
                CreationTime = comment.CreationTime.ToString("dd/MM/yyyy HH:mm");
                LikeCount = comment.Likes != null ? comment.Likes.Count : 0;
                LikeUsers = comment.Likes != null ? comment.Likes.Select(l => new LikeUsersDTO(l)).ToList() : null;
            }
        }
        public string Type { get; set; } = "Comment";
        public int CommentId { get; set; }
        public int ZweetId { get; set; }
        public ZweetDTO Zweet { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhotoPath { get; set; }
        public string Content { get; set; }
        public string CreationTime { get; set; }
        public int LikeCount { get; set; }
        public ICollection<LikeUsersDTO>? LikeUsers { get; set; }
    }
}