using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Interfaces
{
    public interface ICommentRepository
    {
        public bool Save();
        public bool CreateComment(int id, Comment comment);
        public bool DeleteComment(int id);
        public ICollection<Comment> GetCommentsToTask(int id);
    }
}
