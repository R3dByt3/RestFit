using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
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
