using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public partial class LoginCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
