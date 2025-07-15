using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UiAppLayer.Models;

namespace UiAppLayer.Controllers
{
    [Route("AuditRequest")]
    public class AuditRequestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuditRequestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [NonAction]
        private HttpClient GetAuthorizedClient()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = _httpClientFactory.CreateClient("ApiClient"); //Use named client

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        // GET: /AuditRequest
        [HttpGet("", Name = "AuditRequestList")]
        public async Task<IActionResult> Index()
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync("api/v1/AuditRequest/GetAllAuditRequests");
            if (!response.IsSuccessStatusCode)
                return View(new List<AuditRequest>());

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<AuditRequest>>(json);
            return View(list);
        }

        // GET: /AuditRequest/Create
        [HttpGet("Create", Name = "CreateAuditRequest")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /AuditRequest/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuditRequest request)
        {
            Console.WriteLine("POST /AuditRequest/Create called");
            request.RequestDate = DateTime.Now;
            request.Status = "Pending";

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is invalid");
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                    }
                }
                return View(request);
            }

            var client = GetAuthorizedClient();
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/v1/AuditRequest/CreateAuditRequest", content);

            Console.WriteLine("API response: " + response.StatusCode); //  See status
            var apierror = await response.Content.ReadAsStringAsync();     // Read error body
            Console.WriteLine("API error content: " + apierror); // log to console

            if (response.IsSuccessStatusCode)
                return RedirectToRoute("AuditRequestList");

            ModelState.AddModelError("", "Failed to create audit request.");
            return View(request);
        }

        // GET: /AuditRequest/Details/5
        [HttpGet("Details/{id}", Name = "AuditRequestDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/AuditRequest/GetByAuditId/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToRoute("AuditRequestList");

            var json = await response.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<AuditRequest>(json);
            return View(request);
        }

        // POST: /AuditRequest/Delete/5
        [HttpPost("Delete/{id}", Name = "DeleteAuditRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.DeleteAsync($"api/v1/AuditRequest/DeleteByAuditId/{id}");
            return RedirectToRoute("AuditRequestList");
        }

        // GET: /AuditRequest/EditStatus/5
        [HttpGet("EditStatus/{id}", Name = "EditAuditStatus")]
        public async Task<IActionResult> EditStatus(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/AuditRequest/GetByAuditId/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToRoute("AuditRequestList");

            var json = await response.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<AuditRequest>(json);
            return View(request);
        }

        // POST: /AuditRequest/EditStatus/5
        [HttpPost("EditStatus/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatus(int id, string status)
        {
            var client = GetAuthorizedClient();
            var response = await client.PutAsync($"api/v1/AuditRequest/Verify/{id}/status?newStatus={status}", null);

            if (response.IsSuccessStatusCode)
                return RedirectToRoute("AuditRequestList");

            ModelState.AddModelError("", "Failed to update audit request status.");
            return View(); // Optional: Return the original object
        }

    }
}
