using AutoMapper;
using System.Security.Cryptography;
using TaskManagmentSystemApiProject.Data;
using TaskManagmentSystemApiProject.Dto;
using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<User, UserDto>();
            CreateMap<Models.Task, TaskDto>().ConvertUsing(new TaskConverter());
            CreateMap<CommentDto, Comment>().ConvertUsing(new CommentConverter());
            CreateMap<UserDto, User>().ConvertUsing(new UserDtoConverter());
            CreateMap<TaskDto, Models.Task>().ConvertUsing(new TaskDtoConverter());
        }

    }
    public class UserDtoConverter : ITypeConverter<UserDto, User>
    {
        public User Convert(UserDto source, User destination, ResolutionContext context)
        {
            User user = new User();
            user.FirstName = source.FirstName;
            user.LastName = source.LastName;
            using (var hmac = new HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(source.PasswordHash));
            }
            user.Email = source.Email;
            user.Role = 0;
            return user;
        }
    }
    public class TaskDtoConverter : ITypeConverter<TaskDto, Models.Task>
    {
        public Models.Task Convert(TaskDto source, Models.Task destination, ResolutionContext context)
        {
            Models.Task task = new Models.Task();
            task.Title = source.Title;
            task.Description = source.Description;
            task.CreatedBy = new User();
            task.CreatedBy.Id = source.CreatedById;
            task.AssignedTo = new User();
            task.AssignedTo.Id = source.AssignedToId;
            task.Priority = source.Priority;
            task.Status = source.Status;
            task.DueDate = source.DueDate;
            task.CreatedAt = source.CreatedAt;
            task.UpdatedAt = source.UpdatedAt;
            return task;
        }
    }
    public class TaskConverter : ITypeConverter<Models.Task, TaskDto>
    {
        public TaskDto Convert(Models.Task source, TaskDto destination, ResolutionContext context)
        {
            TaskDto dto = new TaskDto();
            dto.Id = source.Id;
            dto.Title = source.Title;
            dto.Description = source.Description;
            dto.CreatedById = source.CreatedBy.Id;
            dto.AssignedToId = source.AssignedTo.Id;
            dto.Priority = source.Priority;
            dto.Status = source.Status;
            dto.DueDate = source.DueDate;
            dto.CreatedAt = source.CreatedAt;
            dto.UpdatedAt = source.UpdatedAt;
            return dto;
        }
    }
    public class CommentConverter : ITypeConverter<CommentDto, Comment>
    {
        public Comment Convert(CommentDto source, Comment destination, ResolutionContext context)
        {
            Comment comment = new Comment(){ Task = new Models.Task() };
            comment.CreatedBy = new User();
            comment.CreatedBy.Id = source.CreatedById;
            comment.CreatedAt = source.CreatedAt;
            comment.Text = source.Text;
            return comment;
        }
    }
    public class CommentDtoConverter : ITypeConverter<Comment, CommentDto>
    {
        public CommentDto Convert(Comment source, CommentDto destination, ResolutionContext context)
        {
            CommentDto comment = new CommentDto();
            comment.Id = source.Id;
            comment.CreatedById = source.CreatedBy.Id;
            comment.TaskId = source.Task.Id;
            comment.CreatedAt = source.CreatedAt;
            comment.Text = source.Text;
            return comment;
        }
    }
}
