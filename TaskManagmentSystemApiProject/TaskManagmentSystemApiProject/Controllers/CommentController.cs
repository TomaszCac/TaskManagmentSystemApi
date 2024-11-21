using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystemApiProject.Dto;
using TaskManagmentSystemApiProject.Interfaces;
using TaskManagmentSystemApiProject.Models;
using TaskManagmentSystemApiProject.Services.UserServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagmentSystemApiProject.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _comrepos;
        private readonly IMapper _mapper;
        private readonly IUserService _service;
        private readonly IUserRepository _userrepos;

        public CommentController(ICommentRepository comrepos, IUserRepository userrepos, IMapper mapper, IUserService service)
        {
            _comrepos = comrepos;
            _mapper = mapper;
            _service = service;
            _userrepos = userrepos;
        }

        // GET api/comments/task/5
        [HttpGet("task/{id}")]
        [Authorize]
        public IActionResult GetCommentsToTask(int id)
        {
            return Ok(_mapper.Map<List<CommentDto>>(_comrepos.GetCommentsToTask(id)));
        }

        // POST api/comments/task/5
        [HttpPost("task/{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Post(int id, [FromBody] CommentDto comment)
        {
            if (comment == null)
            {
                ModelState.AddModelError("", "Data has not been sent");
                return BadRequest(ModelState);
            }
            comment.CreatedById = _userrepos.GetUserByEmail(_service.GetEmail()).Id;
            return Ok(_comrepos.CreateComment(id, _mapper.Map<Comment>(comment)));
        }

        // DELETE api/comments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Delete(int id)
        {
            if (_userrepos.GetUserByEmail(_service.GetEmail()).Id == _comrepos.GetComment(id).CreatedBy.Id || _service.GetRole() == "Admin")
            {
                return Ok(_comrepos.DeleteComment(id));
            }
            ModelState.AddModelError("", "You don't have permission to do that");
            return StatusCode(403, ModelState);

        }
    }
}
