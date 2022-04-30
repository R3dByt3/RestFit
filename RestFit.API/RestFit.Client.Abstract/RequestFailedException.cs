using System.Runtime.Serialization;

namespace RestFit.Client.Abstract
{
    [Serializable]
    public class RequestFailedException : RestFitClientExceptionBase
    {
        public RequestFailedException()
        {
        }

        public RequestFailedException(string? message) : base(message)
        {
        }

        public RequestFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RequestFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
