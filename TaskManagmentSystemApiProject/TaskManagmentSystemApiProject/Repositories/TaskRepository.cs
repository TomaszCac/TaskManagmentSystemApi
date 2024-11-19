using TaskManagmentSystemApiProject.Data;
using TaskManagmentSystemApiProject.Interfaces;

namespace TaskManagmentSystemApiProject.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDatabaseContext _context;

        public TaskRepository(TaskDatabaseContext context)
        {
            _context = context;
        }

        public bool CreateTask(Models.Task task)
        {
            task.CreatedAt = DateTime.Now;
            task.UpdatedAt = DateTime.Now;
            task.AssignedTo = _context.Users.Where(u => u.Id == task.AssignedTo.Id).FirstOrDefault();
            task.CreatedBy = _context.Users.Where(u => u.Id == task.CreatedBy.Id).FirstOrDefault();
            _context.Tasks.Add(task);
            return Save();
        }

        public bool DeleteTask(int id)
        {
            var task = _context.Tasks.Where(t => t.Id == id).FirstOrDefault();
            if (task != null)
            {
                _context.Tasks.Remove(task);
                return Save();
            }
            return false;
        }

        public ICollection<Models.Task> GetAllTasks()
        {
            var entities = _context.Tasks.ToList();
            foreach (var entity in entities)
            {
                var userCreated = _context.Tasks.Where(t => t.Id == entity.Id).Select(t => t.CreatedBy).FirstOrDefault();
                var userAssigned = _context.Tasks.Where(t => t.Id == entity.Id).Select(t => t.AssignedTo).FirstOrDefault();
                entity.CreatedBy = userCreated;
                entity.AssignedTo = userAssigned;
            }
            return entities;

        }

        public Models.Task? GetTaskById(int id)
        {
            var entity = _context.Tasks.Where(t => t.Id == id).FirstOrDefault();
            var created = _context.Tasks.Where(t => t.Id == id).Select(t => t.CreatedBy).FirstOrDefault();
            var assigned = _context.Tasks.Where(t => t.Id == id).Select(t => t.AssignedTo).FirstOrDefault();
            entity.CreatedBy = created;
            entity.AssignedTo = assigned;
            return entity;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateTask(int id, Models.Task task)
        {
            var taskEntity = _context.Tasks.Where(t => t.Id == id).Select(t => t.CreatedAt).FirstOrDefault();
            task.Id = id;
            task.CreatedAt = taskEntity;
            task.UpdatedAt = DateTime.Now;
            task.AssignedTo = _context.Users.Where(u => u.Id == task.AssignedTo.Id).FirstOrDefault();
            task.CreatedBy = _context.Users.Where(u => u.Id == task.CreatedBy.Id).FirstOrDefault();
            _context.Update(task);
            return Save();
        }
    }
}
