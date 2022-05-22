using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class HealthUnitDtoExampleProvider : IExamplesProvider<HealthUnitDto>
    {
        public HealthUnitDto GetExamples()
        {
            return new HealthUnitDto
            {
                Id = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                ArmSize = Random.Shared.NextDouble(),
                WaistSize = Random.Shared.NextDouble(),
                ThightSize = Random.Shared.NextDouble(),
                HipSize = Random.Shared.NextDouble(),
                Weight = Random.Shared.NextDouble(),
                DateUtc = DateTime.UtcNow
            };
        }
    }
}
