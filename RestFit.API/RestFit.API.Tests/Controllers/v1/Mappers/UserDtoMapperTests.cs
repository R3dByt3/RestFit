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

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
        }

        private static IEnumerable<TestCaseData> GetExamples()
        {
            yield return new TestCaseData(
                new User
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
                },
                new UserDto
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
                    }
                ).SetArgDisplayNames("Filled objects");
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void ConvertDto_ShouldReturnExpected(User user, UserDto userDto)
        {
            _mapper.Convert(userDto).Should().BeEquivalentTo(user, opt => opt
            .Using<string[]>(ctx => ctx.Subject.Should().NotBeSameAs(ctx.Expectation).And.BeEquivalentTo(ctx.Expectation))
                .WhenTypeIs<string[]>());
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void Convert_ShouldReturnExpectedDto(User user, UserDto userDto)
        {
            _mapper.Convert(user).Should().BeEquivalentTo(userDto, opt => opt
            .Using<string[]>(ctx => ctx.Subject.Should().NotBeSameAs(ctx.Expectation).And.BeEquivalentTo(ctx.Expectation))
                .WhenTypeIs<string[]>());
        }
    }
}
