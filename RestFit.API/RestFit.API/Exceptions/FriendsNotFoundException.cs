using System.Runtime.Serialization;

namespace RestFit.API.Exceptions
{
    [Serializable]
    public class FriendsNotFoundException : RestFitExceptionBase
    {
        public FriendsNotFoundException()
        {
        }

        public FriendsNotFoundException(string? message) : base(message)
        {
        }

        public FriendsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public FriendsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
