using System.Runtime.Serialization;

namespace RestFit.DataAccess.Abstract.Exceptions
{
    public class IdAlreadyExistsException : RestFitExceptionBase
    {
        public IdAlreadyExistsException()
        {
        }

        public IdAlreadyExistsException(string? message) : base(message)
        {
        }

        public IdAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public IdAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
