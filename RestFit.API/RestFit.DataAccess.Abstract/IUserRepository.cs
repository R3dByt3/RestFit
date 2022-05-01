using RestFit.Data;

namespace RestFit.DataAccess.Abstract
{
    public interface IUserRepository
    {
        public ICollection<User> GetAllUsers();
        public void Insert(User user);
    }
}
