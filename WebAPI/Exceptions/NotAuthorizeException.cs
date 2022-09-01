namespace WebAPI.Exceptions
{
    public class NotAuthorizeException : Exception
    {
        public NotAuthorizeException(string message): 
            base(message)
        {

        }

        public NotAuthorizeException() : base("Unauthorized")
        {

        }
    }
}
