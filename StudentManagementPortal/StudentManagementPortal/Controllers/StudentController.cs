using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
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
    public class StudentController : Controller
    {
        private readonly StudentManagementContext db;

        private static string token;

        private IConfiguration _config;

        readonly log4net.ILog _log4net;

        private readonly ILogger<HomeController> _logger;

        public StudentController(StudentManagementContext context, IConfiguration config)
        {
            db = context;
            _config = config;
            _log4net = log4net.LogManager.GetLogger(typeof(StudentController));
        }
       public async Task<IActionResult> Index(int id)
        {
            BasicDetails list = new BasicDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44307/api/Student/GetDetail/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<BasicDetails>(apiResponse);
                }
            }
            _log4net.Info("Student:Homepage");
            return View(list);
        }

        public async Task<IActionResult> UpdateDetail(int id)
        {
            BasicDetails detail = new BasicDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44307/api/Student/GetDetail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    detail = JsonConvert.DeserializeObject<BasicDetails>(apiResponse);
                }
            }
            return View(detail);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetail(BasicDetails data)
        {
            BasicDetails receivedDetail = new BasicDetails();
            using (var httpClient = new HttpClient())
            {
                var newcontent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, MediaTypeNames.Application.Json);

                using (var response = await httpClient.PutAsync("https://localhost:44307/api/Student/UpdateDetail", newcontent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.result = "Record Updated Successfully!";
                        _log4net.Info("Student:Record Updated");
                        return View();
                    }
                    else
                    {
                        ViewBag.result = "Error in Updating Records Try Again!";
                        return View();
                    }
                }
            }
        }
        public async Task<IActionResult> ViewMarks(int id)
        {
            MarksDetails list = new MarksDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44307/api/Student/GetMarks/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<MarksDetails>(apiResponse);
                }
            }
            _log4net.Info("Student:Marks Viewed");
            return View(list);
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
