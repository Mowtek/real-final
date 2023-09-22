using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class ZweetDTO
    {
        public ZweetDTO() {}

        public ZweetDTO(Zweet zweet)
        {
            if (zweet != null)
            {
                ZweetId = zweet.ZweetId;
                UserId = zweet.UserId;
                if (zweet.User != null)
                {
                    UserPhotoPath = zweet.User.ProfileImagePath;
                    UserName = zweet.User.UserName;
                }
                Content = zweet.Content;
                CreationTime = zweet.CreationTime.ToString("dd/MM/yyyy HH:mm");
                LikeCount = zweet.Likes != null ? zweet.Likes.Count : 0;
                LikeUsers = zweet.Likes != null ? zweet.Likes.Select(l => new LikeUsersDTO(l)).ToList() : null;
                CommentCount = zweet.Comments != null ? zweet.Comments.Count : 0;
                RezweetUsers = zweet.Rezweets != null ? zweet.Rezweets.Select(r => new RezweetUsersDTO(r)).ToList() : null;
                RezweetCount = zweet.Rezweets != null ? zweet.Rezweets.Count : 0;
                Comments = zweet.Comments != null ? zweet.Comments.Select(c => new CommentDTO(c)).OrderByDescending(c => c.CreationTime).ToList() : null;
            }
        }
        public string Type { get; set; } = "Zweet";
        public int ZweetId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhotoPath {  get; set; }
        public string Content { get; set; }
        public string CreationTime { get; set; }
        public int LikeCount { get; set; }
        public ICollection<LikeUsersDTO>? LikeUsers { get; set; }
        public int CommentCount { get; set; }
        public int RezweetCount { get; set; }
        public ICollection<RezweetUsersDTO> RezweetUsers { get; set; }
        public ICollection<CommentDTO>? Comments { get; set; }
    }
}
