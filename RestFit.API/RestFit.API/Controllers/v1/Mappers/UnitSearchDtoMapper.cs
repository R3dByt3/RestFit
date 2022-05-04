using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.API.Controllers.v1.Mappers
{
    public class UnitSearchDtoMapper : DtoMapperBase<UnitSearchDto, UnitSearch>
    {
        public static UnitSearchDtoMapper Instance { get; set; } = new UnitSearchDtoMapper();

        protected override UnitSearchDto ConvertData(UnitSearch element)
        {
            return new UnitSearchDto
            {
                Id = element.Id,
                UserId = element.UserId,
                Type = element.Type
            };
        }

        protected override UnitSearch ConvertData(UnitSearchDto element)
        {
            return new UnitSearch
            {
                Id = element.Id,
                UserId = element.UserId,
                Type = element.Type
            };
        }
    }
}
