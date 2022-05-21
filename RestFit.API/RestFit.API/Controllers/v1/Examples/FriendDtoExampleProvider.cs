using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class FriendDtoExampleProvider : IExamplesProvider<FriendDto>
    {
        public FriendDto GetExamples()
        {
            return new FriendDto
            {
                Name = "Jürgen",
                AverageRepitions = 20,
                AverageSets = 10,
                AverageWeight = 90,
                UserId = Guid.NewGuid().ToString(),
                FriendId = Guid.NewGuid().ToString()
            };
        }
    }
}
