namespace TaskManagmentSystemApiProject.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public int CreatedBy {  get; set; }

        public int AssingedTo { get; set; }

        public enum Priority { Low, Medium, High }

        public enum Status { Pending, InProgress, Completed }

        public DateTime DueDate {  get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
