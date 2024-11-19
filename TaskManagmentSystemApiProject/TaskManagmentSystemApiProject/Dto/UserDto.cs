﻿using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string PasswordHash { get; set; } = "";

        public Role Role { get; set; }

    }
}
