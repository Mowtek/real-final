using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class RezweetUsersDTO
    {
        public RezweetUsersDTO() { } 
        public RezweetUsersDTO(Rezweet rezweet)
        {
            RezweetId = rezweet.RezweetId;
            ZweetId = rezweet.ZweetId;
            UserId = rezweet.UserId;
        }
        public int RezweetId {  get; set; }
        public int ZweetId { get; set; }
        public int UserId { get; set; }
    }
}
