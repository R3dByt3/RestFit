using RestFit.DataAccess.Abstract;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class UserDtoExampleProvider : IExamplesProvider<User>
    {
        public User GetExamples()
        {
            return new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "User2",
                Password = Guid.NewGuid().ToString(),
                FriendUserIds = new string[]
                {
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString()
                },
                PendingInFriendRequestUserIds = new string[]
                {
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString()
                },
                PendingOutFriendRequestUserIds = new string[]
                {
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString()
                }
            };
        }
    }
}
