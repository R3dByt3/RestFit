using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Controllers.v1.Mappers
{
    public class UnitDtoMapper : DtoMapperBase<UnitDto, Unit>
    {
        public static readonly UnitDtoMapper Instance = new();

        protected override UnitDto ConvertData(Unit element)
        {
            return new UnitDto
            {
                Id = element.Id,
                Comment = element.Comment,
                Repetitions = element.Repetitions,
                Type = element.Type,
                UserId = element.UserId,
                Sets = element.Sets,
                Weight = element.Weight,
                DateUtc = element.DateUtc
            };
        }

        protected override Unit ConvertData(UnitDto element)
        {
            return new Unit
            {
                Id = element.Id,
                Comment = element.Comment,
                Repetitions = element.Repetitions,
                Type = element.Type,
                UserId = element.UserId,
                Sets = element.Sets,
                Weight = element.Weight,
                DateUtc = element.DateUtc
            };
        }
    }
}
