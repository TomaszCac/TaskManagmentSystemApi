using System.ComponentModel.DataAnnotations;

namespace TaskManagmentSystemApiProject.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public required Task Task { get; set; }

        public User? CreatedBy { get; set; }

        public string Text { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}
