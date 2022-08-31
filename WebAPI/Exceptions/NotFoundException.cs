namespace WebAPI.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, int StatusCode): base(message)
        {
            this.StatusCode = StatusCode;
        }
        public NotFoundException(string message) : base(message)
        {
            StatusCode = 404;
        }
        public NotFoundException() : base("Not found")
        {
            StatusCode = 404;
        }

        public int StatusCode { get; set; }
    }
}
