using TaskManagmentSystemApiProject.Data;

namespace TaskManagmentSystemApiProject.Repositories
{
    public class UserRepository
    {
        private readonly TaskDatabaseContext _context;

        public UserRepository(TaskDatabaseContext context)
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
