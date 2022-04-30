using RestFit.Client.Abstract;
using RestSharp;
using RestSharp.Authenticators;

namespace RestFit.Client
{
    public abstract class ClientBase : IDisposable
    {
        private static string ClientId = "";
        private static string ClientSecret = "";
        private bool disposedValue;

        protected abstract string BaseUrl { get; }
        private RestClient _client { get; }

        protected ClientBase()
        {
            var options = new RestClientOptions(BaseUrl);
            _client = new RestClient(options)
            {
                Authenticator = new HttpBasicAuthenticator(ClientId, ClientSecret),
            };
        }

        private void AddParams(RestRequest request, ParametersCollection @params)
        {
            foreach (var param in @params)
            {
                request.AddParameter(param);
            }
        }

        private void ThrowOnInvalidResponse(RestResponse response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new RequestFailedException($"StatusCode: {response.StatusCode}; Message: {response.ErrorMessage}");
        }

        private void ThrowOnInvalidResponse<T>(RestResponse<T> response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new RequestFailedException($"StatusCode: {response.StatusCode}; Message: {response.ErrorMessage}");

            if (response.Data == null)
                throw new DataSerializationException($"Response could not be parsed; Content: {response.Content}");
        }

        protected async Task<T> ExecuteGetAsync<T>(string path, ParametersCollection @params)
        {
            var request = new RestRequest(path);
            AddParams(request, @params);

            var response = await _client.ExecuteGetAsync<T>(request).ConfigureAwait(false);

            ThrowOnInvalidResponse(response);

            return response.Data!;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ClientBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
