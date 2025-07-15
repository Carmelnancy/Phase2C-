using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UiAppLayer.Models;

namespace UiAppLayer.Controllers
{
    [Route("ServiceRequest")]
    public class ServiceRequestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceRequestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [NonAction]
        private HttpClient GetAuthorizedClient()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = _httpClientFactory.CreateClient("ApiClient"); // Use named client

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        // GET: /ServiceRequest
        [HttpGet("", Name = "ServiceRequestList")]
        public async Task<IActionResult> Index()
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync("api/v1/ServiceRequest/Getallservicerequests");

            if (!response.IsSuccessStatusCode)
                return View(new List<ServiceRequest>());

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ServiceRequest>>(json);
            return View(list);
        }

        // GET: /ServiceRequest/Details/5
        [HttpGet("Details/{serviceRequestId}", Name = "ServiceRequestDetails")]
        public async Task<IActionResult> Details(int serviceRequestId)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/ServiceRequest/GetByserviceId/{serviceRequestId}");

            if (!response.IsSuccessStatusCode)
                return RedirectToRoute("ServiceRequestList");

            var json = await response.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<ServiceRequest>(json);
            return View(request);
        }

        // POST: /ServiceRequest/UpdateStatus/5
        [HttpPost("UpdateStatus/{serviceRequestId}", Name = "UpdateServiceRequestStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int serviceRequestId, string newStatus)
        {
            var client = GetAuthorizedClient();
            var response = await client.PutAsync($"api/v1/ServiceRequest/UpdateStatus/{serviceRequestId}/status?newStatus={newStatus}",null);
            return RedirectToRoute("ServiceRequestList");
        }
        // POST: /ServiceRequest/Delete/5
        [HttpPost("Delete/{serviceRequestId}", Name = "DeleteServiceRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int serviceRequestId)
        {
            var client = GetAuthorizedClient();
            var response = await client.DeleteAsync($"api/v1/ServiceRequest/DeleteServiceById/{serviceRequestId}");
            return RedirectToRoute("ServiceRequestList");
        }
    }
}
