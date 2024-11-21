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
        public bool VerifyPassword(UserDto user);
        public bool VerifyEmail(string email);
        public string CreateToken(UserDto user);
    }
}
