using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UiAppLayer.Models;

namespace UiAppLayer.Controllers
{
    [Route("Asset")]
    public class AssetController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AssetController(IHttpClientFactory httpClientFactory)
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

        //view all asset
        [HttpGet]
        [Route("", Name = "AssetIndex")]
        public async Task<IActionResult> Index()
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync("api/v1/Asset/GetAllAssets");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Asset>>(data);
                return View(list);
            }

            ViewBag.Error = "Unable to fetch assets.";
            return View(new List<Asset>());
        }


        //adding asset
        [HttpGet]
        [Route("create", Name = "AssetCreate")]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create", Name = "AssetCreatePost")]
        public async Task<IActionResult> Create(Asset asset)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(asset);
            }

            var client = GetAuthorizedClient();
            var json = JsonConvert.SerializeObject(asset);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/v1/Asset/AddAsset", content);
            if (response.IsSuccessStatusCode)
                return RedirectToRoute("AssetIndex");

            ModelState.AddModelError("", "Failed to create asset.");
            await LoadDropdownsAsync();
            return View(asset);
        }

        [NonAction]
        private async Task LoadDropdownsAsync()
        {
            var client = GetAuthorizedClient();

            // Get Categories
            var catRes = await client.GetAsync("api/v1/AssetCategory/GetAllCategory");
            if (catRes.IsSuccessStatusCode)
            {
                var json = await catRes.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<AssetCategory>>(json);
                ViewBag.CategoryList = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();
            }

            // Get Employees
            var empRes = await client.GetAsync("api/v1/Employee/GetAllEmployees");
            if (empRes.IsSuccessStatusCode)
            {
                var json = await empRes.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                ViewBag.EmployeeList = employees.Select(e => new SelectListItem
                {
                    Value = e.EmployeeId.ToString(),
                    Text = e.Name
                }).ToList();
            }
        }


        //Update asset
        [HttpGet]
        [Route("edit/{id}", Name = "AssetEdit")]
        public async Task<IActionResult> Edit(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/Asset/GetAssetById/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var asset = JsonConvert.DeserializeObject<Asset>(json);

            await LoadDropdownsAsync();
            return View(asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}", Name = "AssetEditPost")]
        public async Task<IActionResult> Edit(int id, Asset asset)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(asset);
            }

            var client = GetAuthorizedClient();
            var json = JsonConvert.SerializeObject(asset);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/v1/Asset/UpdateAsset", content);
            if (response.IsSuccessStatusCode)
                return RedirectToRoute("AssetIndex");

            ModelState.AddModelError("", "Failed to update asset.");
            await LoadDropdownsAsync();
            return View(asset);
        }


        //delete asset
        [HttpGet]
        [Route("delete/{id}", Name = "AssetDelete")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/Asset/GetAssetById/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToRoute("AssetIndex");

            var json = await response.Content.ReadAsStringAsync();
            var asset = JsonConvert.DeserializeObject<Asset>(json);
            return View(asset);
        }

        //delete confirmation
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}", Name = "AssetDeleteConfirmed")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.DeleteAsync($"api/v1/Asset/DeleteAssetById/{id}");

            return RedirectToRoute("AssetIndex");
        }


        //View asset by id
        [HttpGet]
        [Route("details/{id}", Name = "AssetDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/Asset/GetAssetById/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load asset details.";
                return RedirectToRoute("AssetIndex");
            }

            var json = await response.Content.ReadAsStringAsync();
            var asset = JsonConvert.DeserializeObject<Asset>(json);
            return View(asset);
        }
    }

}
