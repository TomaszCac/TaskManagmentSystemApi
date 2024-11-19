using TaskManagmentSystemApiProject.Data;

namespace TaskManagmentSystemApiProject.Repositories
{
    public class CommentRepository
    {
        private readonly TaskDatabaseContext _context;

        public CommentRepository(TaskDatabaseContext context)
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
