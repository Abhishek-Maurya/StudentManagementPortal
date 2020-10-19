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
    public class AdminController : Controller
    {
        private readonly StudentManagementContext db;

        private static string token;

        private IConfiguration _config;

        readonly log4net.ILog _log4net;

        private readonly ILogger<HomeController> _logger;

        public AdminController(StudentManagementContext context, IConfiguration config)
        {
            db = context;
            _config = config;
            _log4net = log4net.LogManager.GetLogger(typeof(AdminController));
        }
        public async Task<IActionResult> Index()
        {
            List<BasicDetails> list = new List<BasicDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Admin/GetDetails"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<BasicDetails>>(apiResponse);
                }
            }
            _log4net.Info("Admin:Homepage");
            return View(list);
        }
        public ViewResult Search() => View();

        [HttpPost]
        public async Task<IActionResult> Search(int id)
        {
            BasicDetails bdetail = new BasicDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Admin/GetDetail/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bdetail = JsonConvert.DeserializeObject<BasicDetails>(apiResponse);
                }
            }
            _log4net.Info("Admin: Search Request Completed");
            return View(bdetail);
        }

        public ViewResult AddDetail() => View();

        [HttpPost]
        public async Task<IActionResult> AddDetail(BasicDetails del)
        {
            del.Id= Convert.ToInt32(del.Id);
            BasicDetails newdetail = new BasicDetails();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(del), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Admin/AddDetail", content))
                {
                    if(response.IsSuccessStatusCode)
                    {
                        ViewBag.result = "Record Inserted Successfully!";
                        _log4net.Info("Admin :Added New Student ");
                        return View();
                    }
                    else
                    {
                        ViewBag.result = "Error in Inserting Records Try Again!";
                        return View();
                    }
                }
            }
        }

        public async Task<IActionResult> UpdateDetail(int id)
        {
            BasicDetails detail = new BasicDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Admin/GetDetail/" + id))
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
                var newcontent = new StringContent(JsonConvert.SerializeObject(data),Encoding.UTF8,MediaTypeNames.Application.Json);

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/Admin/UpdateDetail", newcontent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.result = "Record Updated Successfully!";
                        _log4net.Info("Admin :Updated Record of ID: "+data.Id);
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

        [HttpPost]
        public async Task<IActionResult> DeleteDetail(int detailid)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/Admin/DeleteDetail/" + detailid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            _log4net.Info("Admin: Record Deleted");
            return RedirectToAction("Index");
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
