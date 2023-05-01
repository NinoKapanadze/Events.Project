namespace Events.Application.Exceptions
{
    public class InvalidUserException : Exception
    {
        public string Code = "User Information Is Not Correct";

        public InvalidUserException(string errorText) : base(errorText)
        {
        }
    }
}