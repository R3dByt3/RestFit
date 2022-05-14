using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
{
    public class RepositoryHub : IRepositoryHub
    {
        public IUnitRepository UnitRepository { get; }
        public IUserRepository UserRepository { get; }
        public IFriendRepository FriendRepository { get; }

        public RepositoryHub(IUserRepository userRepository, IUnitRepository unitRepository, IFriendRepository friendRepository)
        {
            UnitRepository = unitRepository;
            UserRepository = userRepository;
            FriendRepository = friendRepository;
        }
    }
}
