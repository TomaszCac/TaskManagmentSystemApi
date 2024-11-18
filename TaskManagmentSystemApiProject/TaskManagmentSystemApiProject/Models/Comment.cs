namespace TaskManagmentSystemApiProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int Task { get; set; }

        public int CreatedBy { get; set; }

        public string Text { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}
