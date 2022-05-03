using RestFit.Client.Abstract.v1;

namespace RestFit.Client.v1
{
    public class V1 : IV1
    {
        private readonly IUnitClient _unitClient;

        public IUnitClient UnitClient => _unitClient;

        public V1(string username, string password)
        {
            _unitClient = new UnitClient(username, password);
        }
    }
}
