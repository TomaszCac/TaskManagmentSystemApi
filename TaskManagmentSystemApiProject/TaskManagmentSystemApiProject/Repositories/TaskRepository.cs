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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
