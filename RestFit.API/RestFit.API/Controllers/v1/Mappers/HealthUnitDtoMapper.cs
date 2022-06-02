using RestFit.API.Extensions;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Controllers.v1.Mappers
{
    public class HealthUnitDtoMapper : DtoMapperBase<HealthUnitDto, HealthUnit>
    {
        public static readonly HealthUnitDtoMapper Instance = new();

        protected override HealthUnitDto ConvertData(HealthUnit element)
        {
            return new HealthUnitDto
            {
                Id = element.Id,
                UserId = element.UserId,
                ArmSize = element.ArmSize,
                HipSize = element.HipSize,
                ThightSize = element.ThightSize,
                WaistSize = element.WaistSize,
                Weight = element.Weight,
                DateUtc = element.DateUtc.TruncateDateTimeUtc()
            };
        }

        protected override HealthUnit ConvertData(HealthUnitDto element)
        {
            return new HealthUnit
            {
                Id = element.Id,
                UserId = element.UserId,
                ArmSize = element.ArmSize,
                HipSize = element.HipSize,
                ThightSize = element.ThightSize,
                WaistSize = element.WaistSize,
                Weight = element.Weight,
                DateUtc = element.DateUtc.TruncateDateTimeUtc()
            };
        }
    }
}
