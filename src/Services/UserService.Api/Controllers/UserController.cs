using Microsoft.AspNetCore.Mvc;
using UserService.Api.Abstractions;
using UserService.Api.Entities;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            List<User> users = _userService.GetAll().ToList();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            await _userService.AddAysnc(user);
            await _userService.SaveAysnc();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            User newUser = await _userService.GetByIdAysnc(id);
            return Ok(newUser);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var user = await _userService.GetByIdAysnc(id);

            _userService.Delete(user);
            await _userService.SaveAysnc();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            _userService.Update(user);
            await _userService.SaveAysnc();

            return Ok();
        }
    }
}
