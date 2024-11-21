using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystemApiProject.Dto;
using TaskManagmentSystemApiProject.Interfaces;
using TaskManagmentSystemApiProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagmentSystemApiProject.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userrepos;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userrepos, IMapper mapper)
        {
            _userrepos = userrepos;
            _mapper = mapper;
        }
        // GET: api/users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/users/5
        [HttpGet("{id}")]
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
                ModelState.AddModelError("", "User data has not been sent");
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
        public IActionResult UpdateUserById(int id, [FromBody] UserDto user)
        {
            return Ok(_userrepos.UpdateUser(id,_mapper.Map<User>(user)));
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_userrepos.DeleteUser(id));
        }
    }
}
