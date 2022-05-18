using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Controllers.v1.Mappers
{
    public class UserDtoMapper : DtoMapperBase<UserDto, User>
    {
        public static readonly UserDtoMapper Instance = new();

        protected override UserDto ConvertData(User element)
        {
            return new UserDto
            {
                Id = element.Id,
                Password = element.Password,
                Username = element.Username,
                FriendUserIds = element.FriendUserIds.ToArray(),
                PendingInFriendRequestUserIds = element.PendingInFriendRequestUserIds.ToArray(),
                PendingOutFriendRequestUserIds = element.PendingOutFriendRequestUserIds.ToArray()
            };
        }

        protected override User ConvertData(UserDto element)
        {
            return new User
            {
                Id = element.Id,
                Password = element.Password,
                Username = element.Username,
                FriendUserIds = element.FriendUserIds.ToArray(),
                PendingInFriendRequestUserIds = element.PendingInFriendRequestUserIds.ToArray(),
                PendingOutFriendRequestUserIds = element.PendingOutFriendRequestUserIds.ToArray()
            };
        }
    }
}
