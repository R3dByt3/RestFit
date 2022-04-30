using System.Runtime.Serialization;

namespace RestFit.Client.Abstract
{
    [Serializable]
    public class DataSerializationException : RestFitClientExceptionBase
    {
        public DataSerializationException()
        {
        }

        public DataSerializationException(string? message) : base(message)
        {
        }

        public DataSerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DataSerializationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
