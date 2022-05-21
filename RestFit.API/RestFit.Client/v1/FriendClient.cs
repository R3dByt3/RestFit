using RestFit.Client.Abstract.v1;

namespace RestFit.Client.v1
{
    public class FriendClient : ClientBase, IFriendClient
    {
        protected override string BaseUrl => "Friend";
        
        public FriendClient(string username, string password) : base(username, password)
        {
        }


    }
}
