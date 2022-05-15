using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class UnitDtosExampleProvider : IExamplesProvider<List<UnitDto>>
    {
        public List<UnitDto> GetExamples()
        {
            return new List<UnitDto>
            {
                new UnitDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Repetitions = Random.Shared.NextInt64(),
                    Type = "Squats",
                    UserId = Guid.NewGuid().ToString(),
                    Comment = "Some comment",
                    Sets = Random.Shared.NextInt64(),
                    Weight = Random.Shared.NextDouble()
                },
                new UnitDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Repetitions = Random.Shared.NextInt64(),
                    Type = "SitUps",
                    UserId = Guid.NewGuid().ToString(),
                    Comment = "Some comment",
                    Sets = Random.Shared.NextInt64(),
                    Weight = Random.Shared.NextDouble()
                }
            };
        }
    }
}
