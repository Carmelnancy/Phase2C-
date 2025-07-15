using System.Text;
using Microsoft.AspNetCore.Mvc;
using UiAppLayer.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace UiAppLayer.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [NonAction]
        private HttpClient GetAuthorizedClient()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = _httpClientFactory.CreateClient("ApiClient");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
        //login admin
        [Route("/")]
        [Route("login", Name = "AdminLogin")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login", Name = "AdminLoginPost")]
        public async Task<IActionResult> Login(Admin model)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

            var payload = new
            {
                Email = model.Email,
                Password = model.Password,
                Role = "Admin"
            };

            var response = await client.PostAsJsonAsync("api/v1/Admin/login", payload);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Error: " + error);
                ViewBag.Message = "Invalid Credentials!";
                return View();
            }

            //  Deserialize the token properly
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

            if (tokenObj != null && tokenObj.ContainsKey("token"))
            {
                var token = tokenObj["token"];
                HttpContext.Session.SetString("JWToken", token);
                return RedirectToRoute("AdminDashboard");
            }

            ViewBag.Message = "Login failed.";
            return View();
        }

        //Admin Dashboard
        [HttpGet]
        [Route("dashboard", Name = "AdminDashboard")]
        public async Task<IActionResult> Dashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken")))
                return RedirectToRoute("AdminLogin");

            var client = GetAuthorizedClient();
            var dashboardData = new AdminDashboardViewModel();
            //var token = HttpContext.Session.GetString("JWToken");
            //Console.WriteLine(" Token in Dashboard: " + token);


            var empRes = await client.GetAsync("api/v1/Employee/GetAllEmployees");
            if (empRes.IsSuccessStatusCode)
            {
                var json = await empRes.Content.ReadAsStringAsync();
                dashboardData.EmployeeCount = JsonConvert.DeserializeObject<List<Employee>>(json)?.Count ?? 0;
            }

            var catRes = await client.GetAsync("api/v1/AssetCategory/GetAllCategory");
            if (catRes.IsSuccessStatusCode)
            {
                var json = await catRes.Content.ReadAsStringAsync();
                dashboardData.CategoryCount = JsonConvert.DeserializeObject<List<AssetCategory>>(json)?.Count ?? 0;
            }

            var assetRes = await client.GetAsync("api/v1/Asset/GetAllAssets");
            if (assetRes.IsSuccessStatusCode)
            {
                var json = await assetRes.Content.ReadAsStringAsync();
                dashboardData.AssetCount = JsonConvert.DeserializeObject<List<Asset>>(json)?.Count ?? 0;
            }

            var assetReqRes = await client.GetAsync("api/v1/AssetRequest/GetAllAssetRequests");
            if (assetReqRes.IsSuccessStatusCode)
            {
                var json = await assetReqRes.Content.ReadAsStringAsync();
                dashboardData.AssetRequestCount = JsonConvert.DeserializeObject<List<AssetRequest>>(json)?.Count ?? 0;
            }

            var serviceRes = await client.GetAsync("api/v1/ServiceRequest/Getallservicerequests");
            if (serviceRes.IsSuccessStatusCode)
            {
                var json = await serviceRes.Content.ReadAsStringAsync();
                dashboardData.ServiceRequestCount = JsonConvert.DeserializeObject<List<ServiceRequest>>(json)?.Count ?? 0;
            }

            var auditRes = await client.GetAsync("api/v1/AuditRequest/GetAllAuditRequests");
            if (auditRes.IsSuccessStatusCode)
            {
                var json = await auditRes.Content.ReadAsStringAsync();
                dashboardData.AuditRequestCount = JsonConvert.DeserializeObject<List<AuditRequest>>(json)?.Count ?? 0;
            }

            return View(dashboardData);
        }


        //Admin logout
        [HttpGet]
        [Route("logout", Name = "AdminLogout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToRoute("AdminLogin");
        }
    }
}
