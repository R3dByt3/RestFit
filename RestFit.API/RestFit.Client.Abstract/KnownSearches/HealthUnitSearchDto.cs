namespace RestFit.Client.Abstract.KnownSearches
{
    public class HealthUnitSearchDto : SearchBaseDto<HealthUnitDtoFields>
    {
        public string? UserId
        {
            get => GetFirst(HealthUnitDtoFields.UserId);
            set => SetSingle(HealthUnitDtoFields.UserId, value);
        }

        public DateTime? Date
        {
            get => GetFirstDate(HealthUnitDtoFields.DateUtc);
            set => SetSingle(HealthUnitDtoFields.DateUtc, value);
        }
    }
}
