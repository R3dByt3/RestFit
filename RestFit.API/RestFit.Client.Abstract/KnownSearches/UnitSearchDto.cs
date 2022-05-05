namespace RestFit.DataAccess.Abstract.KnownSearches
{
    public class UnitSearchDto : SearchBaseDto<UnitFieldsDto>
    {
        public string? Id 
        {
            get => GetFirst(UnitFieldsDto.Id);
            set => SetSingle(UnitFieldsDto.Id, value);
        }

        public string? UserId
        {
            get => GetFirst(UnitFieldsDto.UserId);
            set => SetSingle(UnitFieldsDto.UserId, value);
        }

        public string? Type
        {
            get => GetFirst(UnitFieldsDto.Type);
            set => SetSingle(UnitFieldsDto.Type, value);
        }
    }
}
