using TaskManagmentSystemApiProject.Data;
using TaskManagmentSystemApiProject.Interfaces;
using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TaskDatabaseContext _context;

        public CommentRepository(TaskDatabaseContext context)
        {
            _context = context;
        }

        public bool CreateComment(int id, Comment comment)
        {
            var taskEntity = _context.Tasks.Where(b => b.Id == id).FirstOrDefault();
            var userEntity = _context.Users.Where(u => u.Id == comment.CreatedBy.Id).FirstOrDefault();
            if (taskEntity != null)
            {
                comment.Task = taskEntity;
                comment.CreatedBy = userEntity;
                _context.Comments.Add(comment);
                return Save();
            }
            return false;
        }

        public bool DeleteComment(int id)
        {
            var entity = _context.Comments.Where(c => c.Id == id).FirstOrDefault();
            if (entity != null)
            {
                _context.Comments.Remove(entity);
                return Save();
            }
            return false;
        }

        public ICollection<Comment> GetCommentsToTask(int id)
        {
            var entities = _context.Comments.Where(b => b.Task.Id == id).ToList();
            foreach (var entity in entities)
            {
                var taskEntity = _context.Comments.Where(c => c.Id == entity.Id).Select(c => c.Task).FirstOrDefault();
                var userEntity = _context.Comments.Where(c => c.Id == entity.Id).Select(c => c.CreatedBy).FirstOrDefault();
                entity.Task = taskEntity;
                entity.CreatedBy = userEntity;
            }
            return entities;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
