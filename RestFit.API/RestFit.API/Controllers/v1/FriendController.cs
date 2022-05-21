using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.DataAccess.Abstract.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using RestFit.API.Controllers.v1.Examples;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.API.Attributes;
using RestFit.API.Exceptions;

namespace RestFit.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FriendController : ControllerWithExceptionHandling
    {
        private readonly IFriendControllerV1Processor _processor;

        public FriendController(IFriendControllerV1Processor processor, ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<FriendController>())
        {
            _processor = processor;
        }

        [HttpPost]
        [Route("request")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Request gespeichert")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Wenn der Name nicht gefunden werden kann", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> CreateFriendRequestAsync([FromQuery, SwaggerParameter(Description = "Username", Required = true)] string username) => await ExecuteSafeAsync(async () =>
        {
            await _processor.CreateFriendRequestAsync(username).ConfigureAwait(false);
            return Ok();
        }).ConfigureAwait(false);

        [HttpPost]
        [Route("accept")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Request gespeichert")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(FriendDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Wenn der Name nicht gefunden werden kann", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> AcceptFriendRequest([FromQuery, SwaggerParameter(Description = "UserId", Required = true)] string userId) => await ExecuteSafeAsync(async () =>
        {
            return Ok(await _processor.AcceptFriendRequest(userId));
        }).ConfigureAwait(false);

        protected override async Task<IActionResult> HandleExceptionAsync(Exception ex)
        {
            await Task.Yield();

            return ex switch
            {
                InsufficientDataException ide => UnprocessableEntity(GenerateErrorDataFromException(ide)),
                DataAccessMongoDbException me => DataAccessMongoDbException(me),
                UserNotFoundException unfe => BadRequest(GenerateErrorDataFromException(unfe)),
                _ => DefaultErrorResponse(ex)
            };
        }
    }
}
