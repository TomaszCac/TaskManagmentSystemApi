using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public int CreatedById { get; set; }

        public int AssignedToId { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
