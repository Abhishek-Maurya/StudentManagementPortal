using System;
using System.Collections.Generic;

namespace StudentManagementAuthorization.Models
{
    public partial class LoginCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
