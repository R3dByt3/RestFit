using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
{
    public class UserGroupedUnitAccess : BaseAccess<UserGroupedUnit>, IUserGroupedUnitAccess
    {
        public UserGroupedUnitAccess() : base("aggregate_user_grouped_unit")
        {
        }

        protected override void EnsureIndices()
        {
            CreateIndex(true, true, x => x.UserId);
        }

        protected override void EnsureViews()
        {
            //No Views
        }
    }
}
