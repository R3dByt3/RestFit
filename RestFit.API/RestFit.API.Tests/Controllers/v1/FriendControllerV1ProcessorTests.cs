using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using RestFit.API.Controllers.v1;
using RestFit.API.Exceptions;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.Logic.Abstract;
using System.Security.Claims;

namespace RestFit.API.Tests.Controllers.v1
{
    [TestFixture]
    public class FriendControllerV1ProcessorTests
    {
        private IProcessorHub _processorHub = null!;
        private IHttpContextAccessor _httpContextAccessor = null!;
        private FriendControllerV1Processor _processor = null!;

        [SetUp]
        public void Setup()
        {
            _processorHub = Substitute.For<IProcessorHub>();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "Test") }));
            _httpContextAccessor.HttpContext.Returns(context);
            _processor = new FriendControllerV1Processor(_processorHub, _httpContextAccessor);
        }

        [Test]
        public async Task CreateFriendRequestAsync_ShouldThrow_WhenFirstUserNotFound()
        {
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult(default(User)));
            var act = async () => await _processor.CreateFriendRequestAsync("name").ConfigureAwait(false);
            await act.Should().ThrowAsync<UserNotFoundException>();
        }

        [Test]
        public async Task CreateFriendRequestAsync_ShouldThrow_WhenSecondUserNotFound()
        {
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult<User?>(new User()), Task.FromResult<User?>(null));
            var act = async () => await _processor.CreateFriendRequestAsync("name").ConfigureAwait(false);
            await act.Should().ThrowAsync<UserNotFoundException>();
        }

        [Test]
        public async Task CreateFriendRequestAsync_ShouldCauseExpectedCalls()
        {
            var user1 = new User();
            var user2 = new User { Id = "Test" };
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult<User?>(user1), Task.FromResult<User?>(user2));
            await _processor.CreateFriendRequestAsync("name").ConfigureAwait(false);
            await _processorHub.SearchProcessor.Received(1).GetUserAsync(Arg.Is<UserSearch>(x => x.Username == "name"));
            await _processorHub.SearchProcessor.Received(1).GetUserAsync(Arg.Is<UserSearch>(x => x.Id == "Test"));
            await _processorHub.UpdateProcessor.Received(1).CreateFriendRequestAsync(user1, user2);
        }

        [Test]
        public async Task AcceptFriendRequestAsync_ShouldThrow_WhenFirstUserNotFound()
        {
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult(default(User)));
            var act = async () => await _processor.AcceptFriendRequestAsync("name").ConfigureAwait(false);
            await act.Should().ThrowAsync<UserNotFoundException>();
        }

        [Test]
        public async Task AcceptFriendRequestAsync_ShouldThrow_WhenSecondUserNotFound()
        {
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult<User?>(new User()), Task.FromResult<User?>(null));
            var act = async () => await _processor.AcceptFriendRequestAsync("name").ConfigureAwait(false);
            await act.Should().ThrowAsync<UserNotFoundException>();
        }

        [Test]
        public async Task AcceptFriendRequestAsync_ShouldCauseExpectedCalls()
        {
            var user1 = new User();
            var user2 = new User { Id = "Test" };
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult<User?>(user1), Task.FromResult<User?>(user2));
            await _processor.AcceptFriendRequestAsync("name").ConfigureAwait(false);
            await _processorHub.SearchProcessor.Received(1).GetUserAsync(Arg.Is<UserSearch>(x => x.Id == "name"));
            await _processorHub.SearchProcessor.Received(1).GetUserAsync(Arg.Is<UserSearch>(x => x.Id == "Test"));
            await _processorHub.DeleteProcessor.Received(1).DeleteFriendRequestAsync(user1, user2);
            await _processorHub.UpdateProcessor.Received(1).CreateFriendsAsync(user1, user2);
        }

        [Test]
        public async Task GetFriendsAsync_ShouldThrow_WhenUserNotFound()
        {
            var search = new FriendSearchDto();
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult(default(User)));
            var act = async () => await _processor.GetFriendsAsync(search).ConfigureAwait(false);
            await act.Should().ThrowAsync<UserNotFoundException>();
        }

        [Test]
        public async Task GetFriendsAsync_ShouldThrow_WhenFriendsNotFound()
        {
            var search = new FriendSearchDto
            {
                Ids = new[]
                {
                    "1",
                    "2"
                } 
            };
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult<User?>(new User { FriendUserIds = new[] {"1"} }));
            var act = async () => await _processor.GetFriendsAsync(search).ConfigureAwait(false);
            await act.Should().ThrowAsync<FriendsNotFoundException>();
        }

        [Test]
        public async Task GetFriendsAsync_ShouldCauseExpectedCalls()
        {
            var ids = new[]
            {
                "1",
                "2"
            };
            var search = new FriendSearchDto
            {
                Ids = ids
            };
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult<User?>(new User { FriendUserIds = ids }));
            _processorHub.SearchProcessor.GetUserGroupedUnitsAsync(Arg.Any<UserGroupedUnitSearch>()).Returns(Task.FromResult<ICollection<UserGroupedUnit>>(new List<UserGroupedUnit> { new UserGroupedUnit { UserId = "xxx" } }));
            await _processor.GetFriendsAsync(search).ConfigureAwait(false);
            await _processorHub.SearchProcessor.Received(1).GetUserAsync(Arg.Is<UserSearch>(x => x.Id == "Test"));
            await _processorHub.SearchProcessor.Received(1).GetUserGroupedUnitsAsync(Arg.Is<UserGroupedUnitSearch>(x => x.UserIds!.SequenceEqual(ids)));
            await _processorHub.SearchProcessor.Received(1).GetUserAsync(Arg.Is<UserSearch>(x => x.Id == "xxx"));
        }

        [Test]
        public async Task GetFriendsAsync_ShouldReturnExpected()
        {
            var ids = new[]
            {
                "1",
                "2"
            };
            var search = new FriendSearchDto
            {
                Ids = ids
            };
            _processorHub.SearchProcessor.GetUserAsync(Arg.Any<UserSearch>()).Returns(Task.FromResult<User?>(new User { FriendUserIds = ids }), Task.FromResult<User?>(new User { Username = "User1" }), Task.FromResult<User?>(new User { Username = "User2" }));
            List<UserGroupedUnit> userGroupedUnits = new List<UserGroupedUnit> 
            { 
                new UserGroupedUnit 
                { 
                    UserId = "xxx",
                    DocumentCount = 2,
                    RepetitionsSum = 4,
                    SetsSum = 6,
                    WeightsSum = 8,
                    Type = "SitUps"
                },
                new UserGroupedUnit
                {
                    UserId = "xxx",
                    DocumentCount = 2,
                    RepetitionsSum = 4,
                    SetsSum = 6,
                    WeightsSum = 8,
                    Type = "PushUps"
                },
                new UserGroupedUnit
                {
                    UserId = "yyy",
                    DocumentCount = 3,
                    RepetitionsSum = 6,
                    SetsSum = 9,
                    WeightsSum = 12,
                    Type = "SitUps"
                }
            };
            _processorHub.SearchProcessor.GetUserGroupedUnitsAsync(Arg.Any<UserGroupedUnitSearch>()).Returns(Task.FromResult<ICollection<UserGroupedUnit>>(userGroupedUnits));
            var friends = await _processor.GetFriendsAsync(search).ConfigureAwait(false);

            friends.Should().BeEquivalentTo(new List<FriendDto>
            {
                new FriendDto
                {
                    FriendId = "xxx",
                    Name = "User1",
                    UserId = "Test",
                    UnitAggregationDtos = new List<UnitAggregationDto>
                    {
                        new UnitAggregationDto
                        {
                            AverageRepitions = 2,
                            AverageSets = 3,
                            AverageWeight = 4,
                            Type = "SitUps"
                        },
                        new UnitAggregationDto
                        {
                            AverageRepitions = 2,
                            AverageSets = 3,
                            AverageWeight = 4,
                            Type = "PushUps"
                        }
                    }
                },
                new FriendDto
                {
                    FriendId = "yyy",
                    Name = "User2",
                    UserId = "Test",
                    UnitAggregationDtos = new List<UnitAggregationDto>
                    {
                        new UnitAggregationDto
                        {
                            AverageRepitions = 2,
                            AverageSets = 3,
                            AverageWeight = 4,
                            Type = "SitUps"
                        }
                    }
                }
            });
        }
    }
}
