using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class UserDTO
    {
        public UserDTO() { }
        public UserDTO(User user)
        {
            UserId = user.UserId;
            UserName = user.UserName;
            Email = user.Email;
            Bio = user.Bio;
            UserRole = user.UserRole;
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public UserRole UserRole { get; set; }
    }
}
