using RestFit.Data;

namespace RestFit.DataAccess.Abstract
{
    public interface IUserRepository
    {
        public Task<ICollection<User>> GetUsersAsync();
        public Task CreateUserAsync(User user);
    }
}
