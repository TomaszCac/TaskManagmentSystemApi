using AutoMapper;
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
            CreateMap<UserDto, User>();
            CreateMap<TaskDto, Models.Task>();
        }
    }
}
