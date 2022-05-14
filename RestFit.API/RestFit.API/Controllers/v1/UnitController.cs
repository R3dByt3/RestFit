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

namespace RestFit.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UnitController : ControllerWithExceptionHandling
    {
        private readonly IUnitControllerV1Processor _processor;

        public UnitController(IUnitControllerV1Processor processor, ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<UnitController>())
        {
            _processor = processor;
        }

        [HttpPost]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Objekt gespeichert")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(UnitDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Wenn das Objekt unvollständig ist", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerRequestExample(typeof(UnitDto), typeof(UnitDtosExampleProvider))]
        public async Task<IActionResult> AddUnitAsync([FromBody, SwaggerParameter(Description = "Body", Required = true)] UnitDto unitDto) => await ExecuteSafeAsync(async () =>
        {
            var unit = await _processor.CreateUnitAsync(unitDto).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetUnitsAsync), new { Id = unit.Id }, unit);
        });

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
        });

        protected override async Task<IActionResult> HandleExceptionAsync(Exception ex)
        {
            await Task.Yield();

            return ex switch
            {
                InsufficientDataException ide => UnprocessableEntity(GenerateErrorDataFromException(ide)),
                _ => DefaultErrorResponse(ex)
            };
        }
    }
}
