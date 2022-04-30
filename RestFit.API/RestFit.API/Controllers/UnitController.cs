using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestFit.API.Services;
using RestFit.Repository.Abstract;

namespace RestFit.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUnitRepository _unitRepository;

        public UnitController(IUserService userService, IUnitRepository unitRepository)
        {
            _userService = userService;
            _unitRepository = unitRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit([FromBody] Unit unit)
        {
            await Task.Yield();
            _unitRepository.Insert(unit);
            return Ok();
        }
    }
}
