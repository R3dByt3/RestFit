using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using RestFit.API.Controllers.v1.Examples;

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
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(UsersDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> GetMyUser() => await ExecuteSafeAsync(async () =>
        {
            return Ok(await _processor.GetMyUserAsync().ConfigureAwait(false));
        }).ConfigureAwait(false);

        protected override async Task<IActionResult> HandleExceptionAsync(Exception ex)
        {
            await Task.Yield();

            return ex switch
            {
                InsufficientDataException ide => UnprocessableEntity(GenerateErrorDataFromException(ide)),
                DataAccessMongoDbException me => DataAccessMongoDbException(me),
                _ => DefaultErrorResponse(ex)
            };
        }
    }
}
