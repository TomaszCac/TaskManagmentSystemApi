using System.Security.Claims;

namespace TaskManagmentSystemApiProject.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpcontext;

        public UserService(IHttpContextAccessor httpcontext)
        {
            _httpcontext = httpcontext;
        }
        public string GetEmail()
        {
            var result = string.Empty;
            if (_httpcontext.HttpContext != null)
            {
                result = _httpcontext.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
            return result;
        }

        public string GetRole()
        {
            var result = string.Empty;
            if (_httpcontext.HttpContext != null)
            {
                result = _httpcontext.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return result;
        }
    }
}
