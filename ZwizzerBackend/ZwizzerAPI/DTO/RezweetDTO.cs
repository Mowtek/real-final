using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class RezweetDTO
    {
        public RezweetDTO() { }
        public RezweetDTO(Rezweet rezweet)
        {
            if (rezweet != null && rezweet.Zweet != null)
            {
                ZweetId = rezweet.Zweet.ZweetId;
                UserId = rezweet.Zweet.UserId;
                if (rezweet.Zweet.User != null)
                {
                    UserPhotoPath = rezweet.Zweet.User.ProfileImagePath;
                    UserName = rezweet.Zweet.User.UserName;
                }
                Content = rezweet.Zweet.Content;
                CreationTime = rezweet.Zweet.CreationTime.ToString("dd/MM/yyyy HH:mm");
                LikeCount = rezweet.Zweet.Likes != null ? rezweet.Zweet.Likes.Count : 0;
                LikeUsers = rezweet.Zweet.Likes != null ? rezweet.Zweet.Likes.Select(l => new LikeUsersDTO(l)).ToList() : null;
                CommentCount = rezweet.Zweet.Comments != null ? rezweet.Zweet.Comments.Count : 0;
                RezweetCount = rezweet.Zweet.Rezweets != null ? rezweet.Zweet.Rezweets.Count : 0;
                RezweetUsers = rezweet.Zweet.Rezweets != null ? rezweet.Zweet.Rezweets.Select(r => new RezweetUsersDTO(r)).ToList() : null;
                Comments = rezweet.Zweet.Comments != null ? rezweet.Zweet.Comments.Select(c => new CommentDTO(c)).ToList() : null;
            }
        }
        public string Type { get; set; } = "Rezweet";
        public int ZweetId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhotoPath { get; set; }
        public string Content { get; set; }
        public string CreationTime { get; set; }
        public int LikeCount { get; set; }
        public ICollection<LikeUsersDTO>? LikeUsers { get; set; }
        public int CommentCount { get; set; }
        public int RezweetCount { get; set; }
        public ICollection<RezweetUsersDTO>? RezweetUsers { get; set; }
        public ICollection<CommentDTO>? Comments { get; set; }
    }
}
