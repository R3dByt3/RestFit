using RestFit.Data;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserAccess _userAccess;

        public UserRepository(IUserAccess userAccess)
        {
            _userAccess = userAccess;
        }

        public ICollection<User> GetAllUsers()
        {
            return _userAccess.RetrieveDocuments();
        }

        public void Insert(User user)
        {
            _userAccess.InsertDocument(user);
        }
    }
}
