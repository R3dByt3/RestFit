using RestFit.Data;
using RestFit.Repository.Abstract;

namespace RestFit.Repository
{
    public class UserAccess : BaseAccess<User>, IUserAccess
    {
        public UserAccess() : base("User")
        {
        }

        protected override void EnsureIndices()
        {
            CreateIndex(true, true, x => x.Username, x => x.Password);
        }

        protected override void EnsureViews()
        {
            //No Views
        }
    }
}
