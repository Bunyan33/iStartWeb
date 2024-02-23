using iStartWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace iStartWeb.Controllers
{
    public class iStartController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7224/api");
        private readonly HttpClient _Client;

        public iStartController()
        {
            _Client = new HttpClient();
            _Client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "/User/SignIn", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("UsersDB");
                }
                
            }
            catch (Exception e)
            {
                return View();
            }

            return View();
        }

        public IActionResult UsersDB()
        {
            IEnumerable<User> userList = new List<User>();
            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/User/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userList = JsonConvert.DeserializeObject<List<User>>(data);
            }
            return View(userList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "/User/Post", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }catch(Exception e)
            {
                return View();
            }

            return View();
        }
    }
}
