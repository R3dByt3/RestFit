using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.Exceptions;

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
        public async Task<IActionResult> AddUnit([FromBody] Unit unit)
        {
            await Task.Yield();
            _unitRepository.Insert(unit);
            return Ok();
        }

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
