using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentManagementPortal.Models;

namespace StudentManagementPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentManagementContext db;

        private static string token;

        private IConfiguration _config;

        readonly log4net.ILog _log4net;

        private readonly ILogger<HomeController> _logger;

        public HomeController(StudentManagementContext context, IConfiguration config)
        {
            db = context;
            _config = config;
            _log4net = log4net.LogManager.GetLogger(typeof(HomeController));
        }
        public IActionResult Index()
        {
            var user = new LoginCredentials();
            return View("Login", user);
        }

        public ActionResult Authenticate(LoginCredentials user)
        {
            token = GetToken(_config["Links:Authorization"], user);
            _log4net.Info("Home:Trying to generate token");

            if (token == null)
            {
                _log4net.Info("Home:Token Generation Failed Unauthorised User");
                ViewBag.Message = String.Format("Invalid Username or Password");
                return View("Login", user);
            }
            _log4net.Info("Home:Token Successfull Generated for "+user.Role);
            if (user.Role == "Student")
            {
                BasicDetails val = null;
                val = db.BasicDetails.Where(x => x.Email == user.UserName).FirstOrDefault();
                int sid = val.Id;
                return RedirectToAction("Index", "Student",new{ id=sid});
            }
            if (user.Role=="Teacher")
                return RedirectToAction("Index", "Teacher");
            else
                return RedirectToAction("Index", "Admin");
        }

        public static string GetToken(string url, LoginCredentials user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                string name = response.Content.ReadAsStringAsync().Result;
                dynamic details = JObject.Parse(name);
                return details.token;
            }
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
