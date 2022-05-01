using System.Runtime.Serialization;

namespace RestFit.DataAccess.Abstract.Exceptions
{
    [Serializable]
    public abstract class RestFitExceptionBase : Exception
    {
        public RestFitExceptionBase()
        {
        }

        public RestFitExceptionBase(string? message) : base(message)
        {
        }

        public RestFitExceptionBase(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RestFitExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
