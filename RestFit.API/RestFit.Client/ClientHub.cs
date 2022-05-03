using RestFit.Client.Abstract;
using RestFit.Client.Abstract.v1;
using RestFit.Client.v1;

namespace RestFit.Client
{
    public class ClientHub : IClientHub
    {
        private readonly IV1 _v1;

        public IV1 V1 => _v1;

        public ClientHub(string username, string password)
        {
            _v1 = new V1(username, password);
        }
    }
}
