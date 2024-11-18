using System.ComponentModel.DataAnnotations;

namespace TaskManagmentSystemApiProject.Models
{
    public enum Priority { Low, Medium, High }

    public enum Status { Pending, InProgress, Completed }

    public class Task
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public User? CreatedBy {  get; set; }

        public User? AssignedTo { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public DateTime DueDate {  get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
