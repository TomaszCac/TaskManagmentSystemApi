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
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskrepos;
        private readonly IMapper _mapper;
        private readonly IUserService _service;
        private readonly IUserRepository _userrepos;

        public TaskController(ITaskRepository taskrepos, IUserRepository userrepos, IMapper mapper, IUserService service)
        {
            _taskrepos = taskrepos;
            _mapper = mapper;
            _service = service;
            _userrepos = userrepos;
        }
        // GET: api/tasks
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] Status? status, [FromQuery] Priority? priority, [FromQuery] int? assignedTo)
        {
            return Ok(_mapper.Map<List<TaskDto>>(_taskrepos.GetAllTasks(status, priority, assignedTo)));
        }

        // GET api/tasks/5
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<TaskDto>(_taskrepos.GetTaskById(id)));
        }

        // POST api/tasks
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Post([FromBody] TaskDto task)
        {
            if (task == null)
            {
                ModelState.AddModelError("", "Data has not been sent");
                return BadRequest(ModelState);
            }
            task.CreatedById = _userrepos.GetUserByEmail(_service.GetEmail()).Id;
            return Ok(_taskrepos.CreateTask(_mapper.Map<Models.Task>(task)));
        }

        // PUT api/tasks/5
        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Put(int id, [FromBody] TaskDto task)
        {
            if (task == null)
            {
                ModelState.AddModelError("", "Data has not been sent");
                return BadRequest(ModelState);
            }
            if (_userrepos.GetUserByEmail(_service.GetEmail()).Id == _taskrepos.GetTaskById(id).CreatedBy.Id || _service.GetRole() == "Admin")
            {
                return Ok(_taskrepos.UpdateTask(id, _mapper.Map<Models.Task>(task)));
            }
            ModelState.AddModelError("", "You don't have permission to do that");
            return StatusCode(403, ModelState);
        }

        // DELETE api/tasks/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Delete(int id)
        {
            if (_userrepos.GetUserByEmail(_service.GetEmail()).Id == _taskrepos.GetTaskById(id).CreatedBy.Id || _service.GetRole() == "Admin")
            {
                return Ok(_taskrepos.DeleteTask(id));
            }
            ModelState.AddModelError("", "You don't have permission to do that");
            return StatusCode(403, ModelState);

        }
    }
}
