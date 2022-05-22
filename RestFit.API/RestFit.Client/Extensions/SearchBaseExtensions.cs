using RestFit.Client.Abstract.KnownSearches;
using RestSharp;

namespace RestFit.Client.Extensions
{
    public static class SearchBaseExtensions
    {
        public static ParametersCollection GetParameters<T>(this SearchBaseDto<T>? search) where T : Enum
        {
            var parameters = new ParametersCollection();

            if (search == null) return parameters;

            foreach (var kvp in search)
            {
                foreach (var value in kvp.Value)
                {
                    parameters.AddParameter(new QueryParameter(kvp.Key.ToString("G"), value));
                }
            }
            return parameters;
        }
    }
}
