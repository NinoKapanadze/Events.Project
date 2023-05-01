namespace Events.Application.Userz
{
    public class UserLoginRequest
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}