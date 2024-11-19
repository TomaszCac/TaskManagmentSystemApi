using TaskManagmentSystemApiProject.Data;

namespace TaskManagmentSystemApiProject.Repositories
{
    public class TaskRepository
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
