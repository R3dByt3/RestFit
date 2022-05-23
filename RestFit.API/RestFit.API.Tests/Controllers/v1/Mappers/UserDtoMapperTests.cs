using FluentAssertions;
using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Tests.Controllers.v1.Mappers
{
    [TestFixture]
    public class UserDtoMapperTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private UserDtoMapper _mapper = null!;
        private User _user = null!;
        private UserDto _userDto = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
            _user = new User
            {
                Id = "abcdfeg",
                FriendUserIds = new[]
                {
                    "x",
                    "y",
                    "z"
                },
                Password = "secure",
                PendingInFriendRequestUserIds = new[]
                {
                    "a",
                    "b",
                    "c"
                },
                PendingOutFriendRequestUserIds = new[]
                {
                    "e",
                    "f",
                    "g"
                },
                Username = "username"
            };
            _userDto = new UserDto
            {
                Id = "abcdfeg",
                FriendUserIds = new[]
                {
                    "x",
                    "y",
                    "z"
                },
                Password = "secure",
                PendingInFriendRequestUserIds = new[]
                {
                    "a",
                    "b",
                    "c"
                },
                PendingOutFriendRequestUserIds = new[]
                {
                    "e",
                    "f",
                    "g"
                },
                Username = "username"
            };
        }

        [Test]
        public void ConvertDto_ShouldReturnExpected()
        {
            _mapper.Convert(_userDto).Should().BeEquivalentTo(_user);
        }

        [Test]
        public void Convert_ShouldReturnExpectedDto()
        {
            _mapper.Convert(_user).Should().BeEquivalentTo(_userDto);
        }
    }
}
