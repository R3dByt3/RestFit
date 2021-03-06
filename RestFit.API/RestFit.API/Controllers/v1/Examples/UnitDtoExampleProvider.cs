using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class UnitDtoExampleProvider : IExamplesProvider<UnitDto>
    {
        public UnitDto GetExamples()
        {
            return new UnitDto
            {
                Id = Guid.NewGuid().ToString(),
                Repetitions = Random.Shared.NextInt64(),
                Type = "Squats",
                UserId = Guid.NewGuid().ToString(),
                Comment = "Some comment",
                Sets = Random.Shared.NextInt64(),
                Weight = Random.Shared.NextDouble(),
                DateUtc = DateTime.UtcNow
            };
        }
    }
}
