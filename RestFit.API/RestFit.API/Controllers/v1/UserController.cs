using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.Client.Abstract.Model;

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
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto user)
        {
            await _processor.CreateUserAsync(user).ConfigureAwait(false);
            return Ok();
        }

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
