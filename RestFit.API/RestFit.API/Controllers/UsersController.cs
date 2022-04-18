using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.API.Models;
using RestFit.API.Services;
using RestFit.Data;
using RestFit.Repository.Abstract;

namespace RestFit.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserAccess _userAccess;

        public UsersController(IUserService userService, IUserAccess userAccess)
        {
            _userService = userService;
            _userAccess = userAccess;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await Task.Yield();
            _userAccess.InsertDocument(user);
            return Ok();
        }
    }
}
