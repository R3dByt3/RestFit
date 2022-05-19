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
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Request gespeichert")]
        //[SwaggerResponseExample((int)HttpStatusCode.Created, typeof(UnitDtoExampleProvider))] //ToDo: Was auch immer machen
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Wenn der Name nicht gefunden werden kann", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        //[SwaggerRequestExample(typeof(string), typeof(UnitDtosExampleProvider))] //ToDo: Was auch immer machen
        public async Task<IActionResult> CreateFriendRequestAsync([FromQuery] string username) => await ExecuteSafeAsync(async () =>
        {
            await _processor.CreateFriendRequestAsync(username).ConfigureAwait(false);
            return Ok();
        }).ConfigureAwait(false);

        /*
        [HttpGet]
        [Produces("application/json")]
        [MethodQueryParameter(nameof(UnitSearchDto.Id), "Suche anhand von Id")]
        [MethodQueryParameter(nameof(UnitSearchDto.Type), "Suche anhand von Type")]
        [MethodQueryParameter(nameof(UnitSearchDto.UserId), "Suche anhand von UserId")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Alle Unit Objekte", typeof(List<UnitDto>))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(UnitDtosExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> GetUnitsAsync([FromQuery, SwaggerIgnoreParameter] UnitSearchDto search) => await ExecuteSafeAsync(async () =>
        {
            return Ok(await _processor.GetUnitsAsync(search));
        }).ConfigureAwait(false);
        */

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
