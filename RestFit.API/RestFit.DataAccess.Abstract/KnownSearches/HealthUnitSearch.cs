namespace RestFit.DataAccess.Abstract.KnownSearches
{
    public class HealthUnitSearch : SearchBase<HealthUnitFields>
    {
        public string? UserId
        {
            get => GetFirst(HealthUnitFields.UserId);
            set => SetSingle(HealthUnitFields.UserId, value);
        }

        public DateTime? Date
        {
            get => GetFirstDate(HealthUnitFields.DateUtc);
            set => SetSingle(HealthUnitFields.DateUtc, value);
        }
    }
}
