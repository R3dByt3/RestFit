using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using RestFit.API.Controllers.v1.Examples;
using RestFit.API.Exceptions;
using RestFit.API.Attributes;
using RestFit.Client.Abstract.KnownSearches;

namespace RestFit.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerWithExceptionHandling
    {
        private readonly IUserControllerV1Processor _processor;

        public UserController(IUserControllerV1Processor processor, ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<UnitController>())
        {
            _processor = processor;
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto user) => await ExecuteSafeAsync(async () =>
        {
            await _processor.CreateUserAsync(user).ConfigureAwait(false);
            return Ok();
        }).ConfigureAwait(false);

        [HttpGet]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Der authentifizierte Benutzer", typeof(UnitDto))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(UserDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Nicht authorisiert")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> GetMyUserAsync() => await ExecuteSafeAsync(async () =>
        {
            return Ok(await _processor.GetMyUserAsync().ConfigureAwait(false));
        }).ConfigureAwait(false);

        [HttpGet]
        [Route("users")]
        [Produces("application/json")]
        [MethodQueryParameter(nameof(UserSearchDto.Ids), "Suche anhand von Ids")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Die gesuchten Benutzer", typeof(UnitDto))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(UsersDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Nicht authorisiert")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> GetUsersAsync([FromQuery, SwaggerIgnoreParameter] UserSearchDto searchDto) => await ExecuteSafeAsync(async () =>
        {
            return Ok(await _processor.GetUsersAsync(searchDto).ConfigureAwait(false));
        }).ConfigureAwait(false);

        protected override async Task<IActionResult> HandleExceptionAsync(Exception ex)
        {
            await Task.Yield();

            return ex switch
            {
                InsufficientDataException ide => UnprocessableEntity(GenerateErrorDataFromException(ide)),
                DataAccessMongoDbException me => DataAccessMongoDbException(me),
                FriendsNotFoundException fnfe => BadRequest(GenerateErrorDataFromException(fnfe)),
                UserNotFoundException unfe => BadRequest(GenerateErrorDataFromException(unfe)),
                _ => DefaultErrorResponse(ex)
            };
        }
    }
}
