namespace Events.Application.Exceptions
{
    public class NotAdministratorException : Exception
    {
        public string Code = "Only Administrator can do given task";

        public NotAdministratorException(string errorText) : base(errorText)
        {
        }
    }
}