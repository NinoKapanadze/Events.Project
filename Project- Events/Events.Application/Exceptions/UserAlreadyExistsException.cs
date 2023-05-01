namespace Events.Application.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public string Code = "User Already Exists";

        public UserAlreadyExistsException(string errorText) : base(errorText)
        {
        }
    }
}