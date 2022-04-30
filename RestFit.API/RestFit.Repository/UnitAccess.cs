using RestFit.Repository.Abstract;

namespace RestFit.Repository
{
    public class UnitAccess : BaseAccess<Unit>, IUnitAccess
    {
        public UnitAccess() : base("Unit")
        {
        }

        protected override void EnsureIndices()
        {
            CreateIndex(true, true, x => x.Id);
            CreateIndex(false, true, x => x.UserId);
        }

        protected override void EnsureViews()
        {
            //No Views
        }
    }
}
