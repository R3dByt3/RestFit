using System.Diagnostics.CodeAnalysis;

namespace RestFit.API.Controllers.v1.Mappers
{
    public abstract class DtoMapperBase<T1, T2>
    {
        protected abstract T1 ConvertData(T2 element);
        protected abstract T2 ConvertData(T1 element);
        
        [return: NotNullIfNotNull("element")]
        public T1? Convert(T2? element)
        {
            return element is null ? default : ConvertData(element);
        }

        [return: NotNullIfNotNull("element")]
        public T2? Convert(T1? element)
        {
            return element is null ? default : ConvertData(element);
        }
    }
}
