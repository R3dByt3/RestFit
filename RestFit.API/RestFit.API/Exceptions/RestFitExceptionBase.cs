using System.Runtime.Serialization;

namespace RestFit.API.Exceptions
{
    [Serializable]
    public abstract class RestFitExceptionBase : Exception
    {
        protected RestFitExceptionBase()
        {
        }

        protected RestFitExceptionBase(string? message) : base(message)
        {
        }

        protected RestFitExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected RestFitExceptionBase(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
