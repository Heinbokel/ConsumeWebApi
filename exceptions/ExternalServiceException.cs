namespace ConsumeWebApi.exceptions {

    public class ExternalServiceException : Exception
    {
        public ExternalServiceException(string? message) : base(message)
        {
        }
    }

}