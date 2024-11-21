using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TaskManagmentSystemApiProject.Data;
using TaskManagmentSystemApiProject.Dto;
using TaskManagmentSystemApiProject.Interfaces;
using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskDatabaseContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(TaskDatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string CreateToken(UserDto user)
        {
            User userEntity = _context.Users.Where(b => b.Email == user.Email).FirstOrDefault();
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userEntity.Email),
                new Claim(ClaimTypes.Role, userEntity.Role.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(9),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return Save();
            }
            return false;
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUser(int id)
        {
            return _context.Users.Where(e => e.Id == id).FirstOrDefault();
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.Where(e => e.Email == email).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(int id, User user)
        {
            user.Id = id;
            _context.Users.Update(user);
            return Save();
        }

        public bool VerifyEmail(string email)
        {
            if (_context.Users.Any(b => b.Email == email))
                return true;
            else
                return false;
        }

        public bool VerifyPassword(UserDto user)
        {
            using (var hmac = new HMACSHA512(_context.Users.Where(b => b.Email == user.Email).FirstOrDefault().PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.PasswordHash));
                return computedHash.SequenceEqual(_context.Users.Where(b => b.Email == user.Email).FirstOrDefault().PasswordHash);
            }
        }
    }
}
