using System.Runtime.Serialization;

namespace RestFit.DataAccess.Abstract.Exceptions
{
    [Serializable]
    public class InsufficientDataException : RestFitExceptionBase
    {
        public InsufficientDataException()
        {
        }

        public InsufficientDataException(string? message) : base(message)
        {
        }

        public InsufficientDataException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public InsufficientDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
