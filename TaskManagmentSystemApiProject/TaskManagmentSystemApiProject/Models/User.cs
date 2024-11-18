using System.ComponentModel.DataAnnotations;

namespace TaskManagmentSystemApiProject.Models
{
    public enum Role { Admin, User }
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string PasswordHash { get; set; } = "";

        public Role Role { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<Task>? TasksCreated { get; set; }

        public ICollection<Task>? TasksAssigned { get; set; }

    }
}
