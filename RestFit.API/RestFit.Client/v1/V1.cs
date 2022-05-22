using RestFit.Client.Abstract.v1;

namespace RestFit.Client.v1
{
    public class V1 : IV1
    {
        public IUnitClient UnitClient { get; }
        public IUserClient UserClient { get; }
        public IFriendClient FriendClient { get; }
        public IHealthUnitClient HealthUnitClient { get; }

        public V1(string username, string password)
        {
            UnitClient = new UnitClient(username, password);
            UserClient = new UserClient(username, password);
            FriendClient = new FriendClient(username, password);
            HealthUnitClient = new HealthUnitClient(username, password);
        }
    }
}
