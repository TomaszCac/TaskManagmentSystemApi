using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystemApiProject.Dto;
using TaskManagmentSystemApiProject.Interfaces;
using TaskManagmentSystemApiProject.Models;
using TaskManagmentSystemApiProject.Services.UserServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagmentSystemApiProject.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userrepos;
        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public UserController(IUserRepository userrepos, IMapper mapper, IUserService service)
        {
            _userrepos = userrepos;
            _mapper = mapper;
            _service = service;
        }
        // GET: api/users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetUserById(int id)
        {
            return Ok(_mapper.Map<UserDto>(_userrepos.GetUser(id)));
        }

        // POST api/users
        [HttpPost]
        public IActionResult Register([FromBody] UserDto user)
        {
            if (user == null)
            {
                ModelState.AddModelError("", "Data has not been sent");
                return BadRequest(ModelState);
            }
            var userMap = _mapper.Map<User>(user);
            if (_userrepos.VerifyEmail(userMap.Email))
            {
                ModelState.AddModelError("", "This email is taken");
                return StatusCode(409, ModelState);
            }
            if (!_userrepos.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("User has been created");
        }

        //POST api/users/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto request)
        {
            if (request == null)
            {
                ModelState.AddModelError("", "Data has not been sent");
                return BadRequest(ModelState);
            }
            if (!_userrepos.VerifyEmail(request.Email))
            {
                ModelState.AddModelError("", "This user doesn't exist");
                return BadRequest(ModelState);
            }
            if (!_userrepos.VerifyPassword(request))
            {
                ModelState.AddModelError("", "Wrong password");
                return BadRequest(ModelState);
            }
            return Ok(_userrepos.CreateToken(request));
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult UpdateUserById(int id, [FromBody] UserDto user)
        {
            if (user == null)
            {
                ModelState.AddModelError("", "Data has not been sent");
                return BadRequest(ModelState);
            }
            if (_userrepos.GetUserByEmail(_service.GetEmail()) == _userrepos.GetUser(id) || _service.GetRole() == "Admin")
            {
                return Ok(_userrepos.UpdateUser(id, _mapper.Map<User>(user)));
            }
            ModelState.AddModelError("", "You don't have permission to do that");
            return StatusCode(403, ModelState);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Delete(int id)
        {
            if (_userrepos.GetUserByEmail(_service.GetEmail()) == _userrepos.GetUser(id) || _service.GetRole() == "Admin")
            {
                return Ok(_userrepos.DeleteUser(id));
            }
            ModelState.AddModelError("", "You don't have permission to do that");
            return StatusCode(403, ModelState);
            
        }
    }
}
