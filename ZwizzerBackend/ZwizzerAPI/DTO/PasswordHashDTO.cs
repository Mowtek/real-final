using ZwizzerDAL;

namespace ZwizzerAPI.DTO
{
    public class PasswordHashDTO
    {
        public PasswordHashDTO() { }
        public PasswordHashDTO(User user)
        {
            UserName = user.UserName;
            Password = user.HashedPassword;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
