using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.API.Models;
using RestFit.API.Services;
using RestFit.Data;
using RestFit.DataAccess.Abstract;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using RestFit.API.Controllers.v1.Examples;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.Client.Abstract.Model;

namespace RestFit.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerWithExceptionHandling
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(IUserService userService, IUserRepository userRepository, ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<UnitController>())
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Alle User Objekte", typeof(List<User>))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(UserDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.GatewayTimeout, "Wenn es ein Problem mit der Datenbank gibt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.GatewayTimeout, typeof(ErrorDataDtoExampleProvider))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Wenn ein unerwarteter Fehler auftritt", typeof(ErrorDataDto))]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(ErrorDataDtoExampleProvider))]
        public async Task<IActionResult> GetAllAsync() => await ExecuteSafeAsync(async () =>
        {
            var users = await _userService.GetAll();
            return Ok(users);
        });

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] User user)
        {
            await _userRepository.CreateUserAsync(user).ConfigureAwait(false);
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
