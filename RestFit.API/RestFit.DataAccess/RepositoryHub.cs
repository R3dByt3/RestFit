using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
{
    public class RepositoryHub : IRepositoryHub
    {
        public IUnitRepository UnitRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserGroupedUnitRepository UserGroupedUnitRepository { get; }
        public IHealthUnitRepository HealthUnitRepository { get; }

        public RepositoryHub(IUserRepository userRepository, IUnitRepository unitRepository, IUserGroupedUnitRepository userGroupedUnitRepository, IHealthUnitRepository healthUnitRepository)
        {
            UnitRepository = unitRepository;
            UserRepository = userRepository;
            UserGroupedUnitRepository = userGroupedUnitRepository;
            HealthUnitRepository = healthUnitRepository;
        }
    }
}
