using AutoMapper;
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

        public TaskController(ITaskRepository taskrepos, IMapper mapper, IUserService service)
        {
            _taskrepos = taskrepos;
            _mapper = mapper;
            _service = service;
        }
        // GET: api/tasks
        [HttpGet]
        public IActionResult Get([FromQuery] Status? status, [FromQuery] Priority? priority, [FromQuery] int? assignedTo)
        {
            return Ok(_mapper.Map<List<TaskDto>>(_taskrepos.GetAllTasks(status, priority, assignedTo)));
        }

        // GET api/tasks/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<TaskDto>(_taskrepos.GetTaskById(id)));
        }

        // POST api/tasks
        [HttpPost]
        public IActionResult Post([FromBody] TaskDto task)
        {
            return Ok(_taskrepos.CreateTask(_mapper.Map<Models.Task>(task)));
        }

        // PUT api/tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TaskDto task)
        {
            return Ok(_taskrepos.UpdateTask(id, _mapper.Map<Models.Task>(task)));
        }

        // DELETE api/tasks/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_taskrepos.DeleteTask(id));
        }
    }
}
