using Newtonsoft.Json;
using RestFit.Client.Abstract.Exceptions;
using RestFit.Client.Abstract.Model;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace RestFit.Client
{
    public abstract class ClientBase : IDisposable
    {
        private static readonly string Server = "https://localhost:7190/";

        private bool disposedValue;

        protected abstract string BaseUrl { get; }
        private RestClient _client { get; }

        protected ClientBase(string username, string password) //ToDo: Configuration
        {
            var options = new RestClientOptions(Server + BaseUrl);
            _client = new RestClient(options)
            {
                Authenticator = new HttpBasicAuthenticator(username, password),
            };
            _client.UseNewtonsoftJson(new JsonSerializerSettings
            {
                Error = HandleDeserializationError
            });
        }

        private void HandleDeserializationError(object? sender, Newtonsoft.Json.Serialization.ErrorEventArgs errorArgs)
        {
            //var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }

        private void AddParams(RestRequest request, ParametersCollection? @params)
        {
            if (@params == null)
                return;

            foreach (var param in @params)
            {
                request.AddParameter(param);
            }
        }

        private void ThrowOnInvalidResponse(RestResponse response)
        {
            if (!response.IsSuccessful)
            {
                var error = JsonConvert.DeserializeObject<ErrorDataDto>(response.Content ?? string.Empty);

                throw new RequestFailedException($"StatusCode: {response.StatusCode}; Message: {error?.Message}");
            }
        }

        private void ThrowOnInvalidResponse<T>(RestResponse response, T data)
        {
            if (!response.IsSuccessful)
            {
                var error = JsonConvert.DeserializeObject<ErrorDataDto>(response.Content ?? string.Empty);

                throw new RequestFailedException($"StatusCode: {response.StatusCode}; Message: {error?.Message}");
            }

            if (data == null)
                throw new DataSerializationException($"Response could not be parsed; Content: {response.Content}");
        }

        protected async Task<T> ExecuteGetAsync<T>(string? path, ParametersCollection? @params)
        {
            var request = new RestRequest(path, Method.Get);
            AddParams(request, @params);

            var response = await _client.ExecuteGetAsync<T>(request).ConfigureAwait(false);

            var data = response.Data;

            ThrowOnInvalidResponse(response!, data);

            return data!;
        }

        protected async Task ExecutePostAsync(string? path)
        {
            var request = new RestRequest(path, Method.Post);

            var response = await _client.ExecutePostAsync(request).ConfigureAwait(false);

            ThrowOnInvalidResponse(response);
        }

        protected async Task ExecutePostAsync<T>(string? path, T data) where T : class
        {
            var request = new RestRequest(path, Method.Post);
            request.AddHeader("Content-type", "application/json");

            request.AddJsonBody(data);

            var response = await _client.ExecutePostAsync(request).ConfigureAwait(false);

            ThrowOnInvalidResponse(response);
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
