using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class UnitDtoExampleProvider : IExamplesProvider<List<UnitDto>>
    {
        public List<UnitDto> GetExamples()
        {
            return new List<UnitDto>
            {
                new UnitDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Duration = TimeSpan.FromMinutes(Random.Shared.NextDouble()),
                    Repitions = (ulong)Random.Shared.NextInt64(),
                    Type = "Squats",
                    UserId = Guid.NewGuid().ToString(),
                },
                new UnitDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Duration = TimeSpan.FromMinutes(Random.Shared.NextDouble()),
                    Repitions = (ulong)Random.Shared.NextInt64(),
                    Type = "SitUps",
                    UserId = Guid.NewGuid().ToString(),
                }
            };
        }
    }
}
