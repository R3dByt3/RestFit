using RestFit.Client.Abstract.KnownSearches;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.DataAccess.Extensions;

namespace RestFit.API.Controllers.v1.Mappers
{
    public class HealthUnitSearchDtoMapper : DtoMapperBase<HealthUnitSearchDto, HealthUnitSearch>
    {
        public readonly static HealthUnitSearchDtoMapper Instance = new();

        protected override HealthUnitSearchDto ConvertData(HealthUnitSearch element)
        {
            return new HealthUnitSearchDto
            {
                UserId = element.UserId,
                DateUtc = element.DateUtc.TruncateDateTimeUtc()
            };
        }

        protected override HealthUnitSearch ConvertData(HealthUnitSearchDto element)
        {
            return new HealthUnitSearch
            {
                UserId = element.UserId,
                DateUtc = element.DateUtc.TruncateDateTimeUtc()
            };
        }
    }
}
