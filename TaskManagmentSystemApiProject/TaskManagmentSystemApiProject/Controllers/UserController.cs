using AutoMapper;
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
            return Ok(_userrepos.CreateUser(_mapper.Map<User>(user)));
        }

        //POST api/users/login
        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok();
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
