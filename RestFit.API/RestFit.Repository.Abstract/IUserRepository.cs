using RestFit.Data;

namespace RestFit.Repository.Abstract
{
    public interface IUserRepository
    {
        public ICollection<User> GetAllUsers();
        public void Insert(User user);
    }
}
