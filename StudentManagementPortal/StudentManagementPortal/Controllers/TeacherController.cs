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
    public class TeacherController : Controller
    {
        private readonly StudentManagementContext db;

        private static string token;

        private IConfiguration _config;

        readonly log4net.ILog _log4net;

        private readonly ILogger<HomeController> _logger;

        public TeacherController(StudentManagementContext context, IConfiguration config)
        {
            db = context;
            _config = config;
            _log4net = log4net.LogManager.GetLogger(typeof(TeacherController));
        }
        public async Task<IActionResult> Index()
        {
            List<MarksDetails> list = new List<MarksDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44328/api/Teacher/GetList"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<MarksDetails>>(apiResponse);
                }
            }
            _log4net.Info("Teacher:Homepage");
            return View(list);
        }
        public ViewResult Search() => View();

        [HttpPost]
        public async Task<IActionResult> Search(int id)
        {
            MarksDetails bdetail = new MarksDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44328/api/Teacher/GetMarks/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bdetail = JsonConvert.DeserializeObject<MarksDetails>(apiResponse);
                }
            }
            _log4net.Info("Teacher:Search Request Completed");
            return View(bdetail);
        }

        public async Task<IActionResult> UpdateMarks(int id)
        {
            MarksDetails detail = new MarksDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44328/api/Teacher/GetMarks/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    detail = JsonConvert.DeserializeObject<MarksDetails>(apiResponse);
                }
            }
            return View(detail);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMarks(MarksDetails data)
        {
            BasicDetails receivedDetail = new BasicDetails();
            using (var httpClient = new HttpClient())
            {
                var newcontent = new StringContent(JsonConvert.SerializeObject(data),Encoding.UTF8,MediaTypeNames.Application.Json);

                using (var response = await httpClient.PutAsync("https://localhost:44328/api/Teacher/UpdateDetail/", newcontent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.result = "Record Updated Successfully!";
                        _log4net.Info("Teacher:Marks Updated of ID: "+data.Id);
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
