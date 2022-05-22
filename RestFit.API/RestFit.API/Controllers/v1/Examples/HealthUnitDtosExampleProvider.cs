using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class HealthUnitDtosExampleProvider : IExamplesProvider<List<HealthUnitDto>>
    {
        public List<HealthUnitDto> GetExamples()
        {
            return new List<HealthUnitDto>
            {
                new HealthUnitDto
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    ArmSize = Random.Shared.NextDouble(),
                    WaistSize = Random.Shared.NextDouble(),
                    ThightSize = Random.Shared.NextDouble(),
                    HipSize = Random.Shared.NextDouble(),
                    Weight = Random.Shared.NextDouble(),
                    DateUtc = DateTime.UtcNow
                },
                new HealthUnitDto
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    ArmSize = Random.Shared.NextDouble(),
                    WaistSize = Random.Shared.NextDouble(),
                    ThightSize = Random.Shared.NextDouble(),
                    HipSize = Random.Shared.NextDouble(),
                    Weight = Random.Shared.NextDouble(),
                    DateUtc = DateTime.UtcNow
                }
            };
        }
    }
}
