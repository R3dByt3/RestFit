using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Controllers.v1.Mappers
{
    public class UnitDtoMapper : DtoMapperBase<UnitDto, Unit>
    {
        public static readonly UnitDtoMapper Instance = new UnitDtoMapper();

        protected override UnitDto ConvertData(Unit element)
        {
            return new UnitDto
            {
                Id = element.Id,
                Duration = element.Duration,
                Repitions = element.Repitions,
                Type = element.Type,
                UserId = element.UserId
            };
        }

        protected override Unit ConvertData(UnitDto element)
        {
            return new Unit
            {
                Id = element.Id,
                Duration = element.Duration,
                Repitions = element.Repitions,
                Type = element.Type,
                UserId = element.UserId
            };
        }
    }
}
