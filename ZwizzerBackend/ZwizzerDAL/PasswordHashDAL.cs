namespace ZwizzerDAL
{
    public class PasswordHashDAL
    {
        public PasswordHashDAL() { }
        public PasswordHashDAL(User user)
        {
            UserName = user.UserName;
            Password = user.HashedPassword;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
