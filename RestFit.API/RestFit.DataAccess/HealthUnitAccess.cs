using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
{
    public class HealthUnitAccess : BaseAccess<HealthUnit>, IHealthUnitAccess
    {
        public HealthUnitAccess() : base("HealthUnit")
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
