using RestFit.DataAccess.Abstract;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class UserDtoExampleProvider : IExamplesProvider<List<User>>
    {
        public List<User> GetExamples()
        {
            return new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "User1",
                    Password = Guid.NewGuid().ToString(),
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "User2",
                    Password = Guid.NewGuid().ToString(),
                }
            };
        }
    }
}
