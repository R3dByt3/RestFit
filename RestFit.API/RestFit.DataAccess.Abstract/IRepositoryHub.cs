namespace RestFit.DataAccess.Abstract
{
    public interface IRepositoryHub
    {
        public IUnitRepository UnitRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserGroupedUnitRepository UserGroupedUnitRepository { get; }
    }
}
