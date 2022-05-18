using System.Runtime.Serialization;

namespace RestFit.API.Exceptions
{
    [Serializable]
    public class UserNotFoundException : RestFitExceptionBase
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }

        public UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
