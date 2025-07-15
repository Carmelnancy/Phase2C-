using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UiAppLayer.Models;

namespace UiAppLayer.Controllers
{
    [Route("AssetRequest")]
    public class AssetRequestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AssetRequestController(IHttpClientFactory httpClientFactory)
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

        // GET: /AssetRequest/List
        [HttpGet("List", Name = "AssetRequestList")]
        public async Task<IActionResult> List()
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync("api/v1/AssetRequest/GetAllAssetRequests");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to fetch asset requests.";
                return View(new List<AssetRequest>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var requests = JsonConvert.DeserializeObject<List<AssetRequest>>(json);
            return View(requests);
        }

        // GET: /AssetRequest/Details/5
        [HttpGet("Details/{id}", Name = "AssetRequestDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/AssetRequest/GetAssetRequestById/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToRoute("AssetRequestList");
            }

            var json = await response.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<AssetRequest>(json);
            return View(request);
        }

        // POST: /AssetRequest/Approve/5
        [HttpPost("Approve/{id}", Name = "ApproveAssetRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.PutAsync($"api/v1/AssetRequest/approve/{id}", null);
            return RedirectToRoute("AssetRequestList");
        }

        // POST: /AssetRequest/UpdateStatus/5
        [HttpPost("UpdateStatus/{id}", Name = "UpdateAssetRequestStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string newStatus)
        {
            var client = GetAuthorizedClient();
            var response = await client.PutAsync($"api/v1/AssetRequest/{id}/status?newStatus={newStatus}",null);

            return RedirectToRoute("AssetRequestList");
        }

        // POST: /AssetRequest/Delete/5
        [HttpPost("Delete/{id}", Name = "DeleteAssetRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.DeleteAsync($"api/v1/AssetRequest/{id}");
            return RedirectToRoute("AssetRequestList");
        }
    }
}
