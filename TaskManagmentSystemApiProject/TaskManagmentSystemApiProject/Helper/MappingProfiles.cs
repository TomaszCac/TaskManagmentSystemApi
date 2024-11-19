using AutoMapper;
using System.Security.Cryptography;
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
            CreateMap<Models.Task, TaskDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<UserDto, User>().ConvertUsing(new UserDtoConverter());
            CreateMap<TaskDto, Models.Task>();
        }

    }
    public class UserDtoConverter : ITypeConverter<UserDto, User>
    {
        public User Convert(UserDto source, User destination, ResolutionContext context)
        {
            User user = new User();
            user.Id = 0;
            user.FirstName = source.FirstName;
            user.LastName = source.LastName;
            user.Email = source.Email;
            user.PasswordHash = source.PasswordHash;
            user.Role = 0;
            return user;
        }
    }
}
