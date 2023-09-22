using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class LikeDTO
    {
        public LikeDTO() {}
        public LikeDTO(Like like)
        {
            if (like != null)
            {
                if (like.Zweet != null && like.Comment == null)
                {
                    ZweetId = like.Zweet.ZweetId;
                    UserId = like.Zweet.UserId;
                    Zweet = new ZweetDTO(like.Zweet);
                    if (like.Zweet.User != null)
                    {
                        UserPhotoPath = like.Zweet.User.ProfileImagePath;
                        UserName = like.Zweet.User.UserName;
                    }
                    Content = like.Zweet.Content;
                    CreationTime = like.Zweet.CreationTime.ToString("dd/MM/yyyy HH:mm");
                    LikeCount = like.Zweet.Likes != null ? like.Zweet.Likes.Count : 0;
                    LikeUsers = like.Zweet.Likes != null ? like.Zweet.Likes.Select(l => new LikeUsersDTO(l)).ToList() : null;
                    CommentCount = like.Zweet.Comments != null ? like.Zweet.Comments.Count : 0;
                    RezweetCount = like.Zweet.Rezweets != null ? like.Zweet.Rezweets.Count : 0;
                    RezweetUsers = like.Zweet.Rezweets != null ? like.Zweet.Rezweets.Select(r => new RezweetUsersDTO(r)).ToList() : null;
                    Comments = like.Zweet.Comments != null ? like.Zweet.Comments.Select(c => new CommentDTO(c)).ToList() : null;
                }
                if (like.Comment != null && like.Zweet == null)
                {
                    CommentId = like.Comment.CommentId;
                    UserId = like.Comment.UserId;
                    Comment = new CommentDTO(like.Comment);
                    if (like.Comment.User != null)
                    {
                        UserPhotoPath = like.Comment.User.ProfileImagePath;
                        UserName = like.Comment.User.UserName;
                    }
                    if (like.Comment.Zweet != null)
                    {
                        Zweet = new ZweetDTO(like.Comment.Zweet);
                        ZweetId = like.Comment.ZweetId;
                    }
                    Content = like.Comment.Content;
                    CreationTime = like.Comment.CreationTime.ToString("dd/MM/yyyy HH:mm");
                    LikeUsers = like.Comment.Likes != null ? like.Comment.Likes.Select(l => new LikeUsersDTO(l)).ToList() : null;
                    LikeCount = like.Comment.Likes != null ? like.Comment.Likes.Count : 0;
                }
            }
        }
        public string Type { get; set; } = "Like";
        public int? ZweetId { get; set; }
        public ZweetDTO Zweet { get; set; }
        public int? CommentId { get; set; }
        public CommentDTO Comment { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhotoPath { get; set; }
        public string Content { get; set; }
        public string CreationTime { get; set; }
        public int LikeCount { get; set; }
        public ICollection<LikeUsersDTO>? LikeUsers { get; set; }
        public int? CommentCount { get; set; }
        public int? RezweetCount { get; set; }
        public ICollection<RezweetUsersDTO>? RezweetUsers { get; set; }
        public ICollection<CommentDTO>? Comments { get; set; }
    }
}
