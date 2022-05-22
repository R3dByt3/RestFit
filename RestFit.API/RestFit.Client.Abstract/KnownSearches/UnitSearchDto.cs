namespace RestFit.Client.Abstract.KnownSearches
{
    public class UnitSearchDto : SearchBaseDto<UnitDtoFields>
    {
        public string? Id
        {
            get => GetFirst(UnitDtoFields.Id);
            set => SetSingle(UnitDtoFields.Id, value);
        }

        public string? UserId
        {
            get => GetFirst(UnitDtoFields.UserId);
            set => SetSingle(UnitDtoFields.UserId, value);
        }

        public string? Type
        {
            get => GetFirst(UnitDtoFields.Type);
            set => SetSingle(UnitDtoFields.Type, value);
        }
    }
}
