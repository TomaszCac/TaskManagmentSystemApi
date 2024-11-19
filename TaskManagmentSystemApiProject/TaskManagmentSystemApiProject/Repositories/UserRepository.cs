using TaskManagmentSystemApiProject.Data;
using TaskManagmentSystemApiProject.Dto;
using TaskManagmentSystemApiProject.Interfaces;
using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskDatabaseContext _context;

        public UserRepository(TaskDatabaseContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return Save();
            }
            return false;
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUser(int id)
        {
            return _context.Users.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(int id, User user)
        {
            user.Id = id;
            _context.Users.Update(user);
            return Save();
        }
    }
}
