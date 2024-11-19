using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Interfaces
{
    public interface ITaskRepository
    {
        public bool Save();
        public ICollection<Models.Task> GetAllTasks(Status? status, Priority? priority, int? assignedTo);
        public Models.Task? GetTaskById(int id);
        public bool CreateTask(Models.Task task);
        public bool DeleteTask(int id);
        public bool UpdateTask(int id, Models.Task task);
    }
}
