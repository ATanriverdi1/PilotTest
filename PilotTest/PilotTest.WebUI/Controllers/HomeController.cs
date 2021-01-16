using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PilotTest.WebUI.Helper;
using PilotTest.WebUI.Models;

namespace PilotTest.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        APIHelper api = new APIHelper();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<StudentData> students = new List<StudentData>();
            HttpClient client = api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync("api/student");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<StudentData>>(result);
            }
            return View(students);
        }

        public async Task<IActionResult> Detail(int id)
        {
            StudentData studentData = new StudentData();
            HttpClient client = api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync($"api/student/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                studentData = JsonConvert.DeserializeObject<StudentData>(result);
            }
            return View(studentData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentData studentData)
        {
            HttpClient client = api.Initial();

            var postTask = await client.PostAsJsonAsync<StudentData>("api/student", studentData);
            if (postTask.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            
            return View(studentData);
        }

        public async Task<IActionResult> Delete(int id)
        {
            StudentData student = new StudentData();
            HttpClient client = api.Initial();
            HttpResponseMessage responseMessage = await client.DeleteAsync($"api/student/{id}");

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
