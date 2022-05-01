using System.Runtime.Serialization;

namespace RestFit.Client.Abstract.Exceptions
{
    [Serializable]
    public abstract class RestFitClientExceptionBase : Exception
    {
        protected RestFitClientExceptionBase()
        {
        }

        protected RestFitClientExceptionBase(string? message) : base(message)
        {
        }

        protected RestFitClientExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected RestFitClientExceptionBase(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
