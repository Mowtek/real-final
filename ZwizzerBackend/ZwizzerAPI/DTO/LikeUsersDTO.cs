using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class LikeUsersDTO
    {
        public LikeUsersDTO() {}
        public LikeUsersDTO(Like like)
        {
            LikeId = like.LikeId;
            UserId = like.UserId;
            ZweetId = like.ZweetId;
            CommentId = like.CommentId;
        }
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int? ZweetId { get; set; }
        public int? CommentId { get; set; }
    }
}
