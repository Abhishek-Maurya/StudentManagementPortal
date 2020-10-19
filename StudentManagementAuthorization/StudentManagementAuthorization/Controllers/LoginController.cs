using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManagementAuthorization.Models;

namespace StudentManagementAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private IConfiguration _config;
        private readonly StudentManagementContext db;
        readonly log4net.ILog _log4net;
        public LoginController(StudentManagementContext _db)
        {
             db = _db;
            _log4net = log4net.LogManager.GetLogger(typeof(LoginController));
        }
        [HttpPost]
        [Route("userlogin")]
        public IActionResult userLogin([FromBody] LoginCredentials newuser)
        {
            _log4net.Info(" Authorization Service Started");
            IActionResult response = Unauthorized();
            var user = AuthenticteUser(newuser);
            if (user != null)
            {
                var tokenstr = GenerateJSONWebToken();
                _log4net.Info(" User Authorised and Token Generated");
                response = Ok(new { token = tokenstr });
            }
            _log4net.Info(" User Unauthorised");
            return response;
        }
        string AuthenticteUser(LoginCredentials login)
        {
            LoginCredentials val = null;
            val = db.LoginCredentials.Where(x => x.UserName == login.UserName && x.Password == login.Password && x.Role==login.Role).FirstOrDefault();
            if (val != null)
               return val.Role;
            else
                return null;
        }
        string GenerateJSONWebToken()
        {
            string key = "ProjectSecretKey";
            var issuer = "StudentManagementSystem";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer,
                issuer, null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}
