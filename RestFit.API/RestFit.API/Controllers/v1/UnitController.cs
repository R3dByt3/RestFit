using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using RestFit.API.Controllers.v1.Examples;
using RestFit.Client.Abstract.Model;
using RestFit.API.Controllers.v1.Mappers;

namespace RestFit.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UnitController : ControllerWithExceptionHandling
    {
        private readonly IUnitRepository _unitRepository;

        public UnitController(IUnitRepository unitRepository, ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<UnitController>())
        {
            _unitRepository = unitRepository;
        }

        [HttpPost]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Objekt gespeichert")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Wenn das Objekt unvollständig ist", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> AddUnitAsync([FromBody] UnitDto unit) => await ExecuteSafeAsync(async () =>
        {
            await Task.Yield();
            _unitRepository.Insert(UnitDtoMapper.Instance.Convert(unit));
            return Ok();
        });

        [HttpGet]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Alle Unit Objekte", typeof(List<UnitDto>))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(UnitDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> GetUnitsAsync() => await ExecuteSafeAsync(async () =>
        {
            await Task.Yield();
            return Ok(_unitRepository.GetAll().Select(x => UnitDtoMapper.Instance.Convert(x)));
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
