using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RestFit.API.Controllers.v1;
using RestFit.API.Exceptions;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.Exceptions;
using System.Net;

namespace RestFit.API.Tests.Controllers.v1
{
    [TestFixture]
    public class FriendControllerTests
    {
        private IFriendControllerV1Processor _processor = null!;
        private ILoggerFactory _factory = null!;
        private ILogger<FriendController> _logger = null!;
        private FriendController _controller = null!;

        [SetUp]
        public void Setup()
        {
            _processor = Substitute.For<IFriendControllerV1Processor>();
            _factory = Substitute.For<ILoggerFactory>();
            _logger = Substitute.For<ILogger<FriendController>>();
            _factory.CreateLogger<FriendController>().Returns(_logger);
            _controller = new FriendController(_processor, _factory);
        }

        private static IEnumerable<TestCaseData> ExceptionsStatusCodes()
        {
            yield return new TestCaseData(new InsufficientDataException("Error"), new StatusCodeResult((int)HttpStatusCode.UnprocessableEntity)).SetArgDisplayNames($"{nameof(InsufficientDataException)}-{nameof(HttpStatusCode.UnprocessableEntity)}");
            yield return new TestCaseData(new DataAccessMongoDbException("Error"), new StatusCodeResult((int)HttpStatusCode.GatewayTimeout)).SetArgDisplayNames($"{nameof(DataAccessMongoDbException)}-{nameof(HttpStatusCode.GatewayTimeout)}");
            yield return new TestCaseData(new UserNotFoundException("Error"), new StatusCodeResult((int)HttpStatusCode.BadRequest)).SetArgDisplayNames($"{nameof(UserNotFoundException)}-{nameof(HttpStatusCode.BadRequest)}");
            yield return new TestCaseData(new FriendsNotFoundException("Error"), new StatusCodeResult((int)HttpStatusCode.BadRequest)).SetArgDisplayNames($"{nameof(FriendsNotFoundException)}-{nameof(HttpStatusCode.BadRequest)}");
            yield return new TestCaseData(new Exception("Error"), new StatusCodeResult((int)HttpStatusCode.InternalServerError)).SetArgDisplayNames($"{nameof(Exception)}-{nameof(HttpStatusCode.InternalServerError)}");
        }

        [Test]
        [TestCaseSource(nameof(ExceptionsStatusCodes))]
        public async Task CreateFriendRequestAsync_Throws_ShouldReturnExpectedStatusCode(Exception ex, StatusCodeResult expected)
        {
            _processor.CreateFriendRequestAsync(Arg.Any<string>()).Returns(_ => throw ex);
            var result = await _controller.CreateFriendRequestAsync("user").ConfigureAwait(false);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task CreateFriendRequestAsync_ShouldReturnExpected()
        {
            _processor.CreateFriendRequestAsync(Arg.Any<string>()).Returns(_ => Task.FromResult(new OkResult()));
            var result = await _controller.CreateFriendRequestAsync("user").ConfigureAwait(false);

            result.Should().BeEquivalentTo(new OkResult());
        }

        [Test]
        [TestCaseSource(nameof(ExceptionsStatusCodes))]
        public async Task AcceptFriendRequestAsync_Throws_ShouldReturnExpectedStatusCode(Exception ex, StatusCodeResult expected)
        {
            _processor.AcceptFriendRequestAsync(Arg.Any<string>()).Returns(_ => throw ex);
            var result = await _controller.AcceptFriendRequestAsync("user").ConfigureAwait(false);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task AcceptFriendRequestAsync_ShouldReturnExpected()
        {
            _processor.AcceptFriendRequestAsync(Arg.Any<string>()).Returns(_ => Task.FromResult(new OkResult()));
            var result = await _controller.AcceptFriendRequestAsync("user").ConfigureAwait(false);

            result.Should().BeEquivalentTo(new OkResult());
        }

        [Test]
        [TestCaseSource(nameof(ExceptionsStatusCodes))]
        public async Task GetFriendsAsync_Throws_ShouldReturnExpectedStatusCode(Exception ex, StatusCodeResult expected)
        {
            _processor.GetFriendsAsync(Arg.Any<FriendSearchDto>()).Returns<List<FriendDto>>(_ => throw ex);
            var result = await _controller.GetFriendsAsync(new FriendSearchDto()).ConfigureAwait(false);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetFriendsAsync_ShouldReturnExpected()
        {
            var expected = new List<FriendDto>
            {
                new FriendDto()
            };
            _processor.GetFriendsAsync(Arg.Any<FriendSearchDto>()).Returns(_ => Task.FromResult(expected));
            var result = await _controller.GetFriendsAsync(new FriendSearchDto()).ConfigureAwait(false);

            result.Should().BeEquivalentTo(new OkObjectResult(expected));
        }
    }
}
