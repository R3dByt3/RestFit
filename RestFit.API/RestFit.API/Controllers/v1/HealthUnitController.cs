using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.DataAccess.Abstract.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using RestFit.API.Controllers.v1.Examples;
using RestFit.Client.Abstract.Model;
using RestFit.API.Attributes;
using RestFit.Client.Abstract.KnownSearches;

namespace RestFit.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HealthUnitController : ControllerWithExceptionHandling
    {
        private readonly IHealthUnitControllerV1Processor _processor;

        public HealthUnitController(IHealthUnitControllerV1Processor processor, ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<HealthUnitController>())
        {
            _processor = processor;
        }

        [HttpPost]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Objekt gespeichert")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(HealthUnitDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Wenn das Objekt unvollständig ist", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Nicht authorisiert")]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerRequestExample(typeof(HealthUnitDto), typeof(HealthUnitDtosExampleProvider))]
        public async Task<IActionResult> AddHealthUnitAsync([FromBody, SwaggerParameter(Description = "Body", Required = true)] HealthUnitDto HealthUnitDto) => await ExecuteSafeAsync(async () =>
        {
            var HealthUnit = await _processor.CreateHealthUnitAsync(HealthUnitDto).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetHealthUnitsAsync), new { Id = HealthUnit.Id }, HealthUnit);
        }).ConfigureAwait(false);

        [HttpGet]
        [Produces("application/json")]
        [MethodQueryParameter(nameof(HealthUnitSearchDto.UserId), "Suche anhand von UserId")]
        [MethodQueryParameter(nameof(HealthUnitSearchDto.DateUtc), "Suche anhand von Date")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Alle HealthUnit Objekte", typeof(List<HealthUnitDto>))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(HealthUnitDtosExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Nicht authorisiert")]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> GetHealthUnitsAsync([FromQuery, SwaggerIgnoreParameter] HealthUnitSearchDto search) => await ExecuteSafeAsync(async () =>
        {
            return Ok(await _processor.GetHealthUnitsAsync(search).ConfigureAwait(false));
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
