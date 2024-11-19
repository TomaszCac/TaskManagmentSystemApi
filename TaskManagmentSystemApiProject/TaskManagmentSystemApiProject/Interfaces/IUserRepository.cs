using TaskManagmentSystemApiProject.Dto;
using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Interfaces
{
    public interface IUserRepository
    {
        public bool Save();
        public ICollection<User> GetAllUsers();
        public bool CreateUser(User user);
        public User? GetUser(int id);
        public bool UpdateUser(int id, User user);
        public bool DeleteUser(int id);
    }
}
