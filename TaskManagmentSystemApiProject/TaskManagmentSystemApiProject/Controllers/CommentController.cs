using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystemApiProject.Dto;
using TaskManagmentSystemApiProject.Interfaces;
using TaskManagmentSystemApiProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagmentSystemApiProject.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _comrepos;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository comrepos, IMapper mapper)
        {
            _comrepos = comrepos;
            _mapper = mapper;
        }

        // GET api/comments/task/5
        [HttpGet("task/{id}")]
        public IActionResult GetCommentsToTask(int id)
        {
            return Ok(_mapper.Map<List<CommentDto>>(_comrepos.GetCommentsToTask(id)));
        }

        // POST api/comments/task/5
        [HttpPost("task/{id}")]
        public IActionResult Post(int id, [FromBody] CommentDto comment)
        {
            return Ok(_comrepos.CreateComment(id, _mapper.Map<Comment>(comment)));
        }

        // DELETE api/comments/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_comrepos.DeleteComment(id));
        }
    }
}
