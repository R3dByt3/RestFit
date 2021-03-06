using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
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
            CreateIndex(false, true, x => x.DateUtc);
        }

        protected override void EnsureViews()
        {
            //No Views
        }
    }
}
