using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
{
    public class RepositoryHub : IRepositoryHub
    {
        public IUnitRepository UnitRepository { get; }

        public IUserRepository UserRepository { get; }

        public RepositoryHub(IUserRepository userRepository, IUnitRepository unitRepository)
        {
            UnitRepository = unitRepository;
            UserRepository = userRepository;
        }
    }
}
