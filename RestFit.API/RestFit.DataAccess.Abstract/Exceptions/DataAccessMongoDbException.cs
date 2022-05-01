using System.Runtime.Serialization;

namespace RestFit.DataAccess.Abstract.Exceptions
{
    [Serializable]
    public class DataAccessMongoDbException : RestFitExceptionBase
    {
        public DataAccessMongoDbException()
        {
        }

        public DataAccessMongoDbException(string? message) : base(message)
        {
        }

        public DataAccessMongoDbException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public DataAccessMongoDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
