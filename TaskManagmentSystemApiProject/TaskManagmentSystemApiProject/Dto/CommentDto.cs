using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }

        public required int TaskId { get; set; }

        public int CreatedById { get; set; }

        public string Text { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}
