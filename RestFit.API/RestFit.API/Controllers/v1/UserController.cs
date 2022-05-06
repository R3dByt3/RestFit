using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.Client.Abstract.Model;
using System.Security.Claims;

namespace RestFit.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerWithExceptionHandling
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserControllerV1Processor _processor;

        public UserController(IHttpContextAccessor httpContextAccessor, IUserControllerV1Processor processor, ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<UnitController>())
        {
            _httpContextAccessor = httpContextAccessor;
            _processor = processor;
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto user) => await ExecuteSafeAsync(async () =>
        {
            await _processor.CreateUserAsync(user).ConfigureAwait(false);
            return Ok();
        });

        [HttpGet]
        public async Task<IActionResult> GetMyUser() => await ExecuteSafeAsync(async () =>
        {
            await Task.Yield();

            return Ok(new UserDto 
            {
                Id = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                Username = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Name)!.Value
            });
        });

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
