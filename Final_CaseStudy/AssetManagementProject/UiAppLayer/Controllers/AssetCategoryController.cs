using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UiAppLayer.Models;

namespace UiAppLayer.Controllers
{

    [Route("AssetCategory")]
    public class AssetCategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AssetCategoryController(IHttpClientFactory httpClientFactory)
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

        //view all category
        [HttpGet]
        [Route("", Name = "AssetCategoryIndex")]
        public async Task<IActionResult> Index()
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync("api/v1/AssetCategory/GetAllCategory");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<AssetCategory>>(json);
                return View(data);
            }

            ViewBag.Error = "Failed to load asset categories.";
            return View(new List<AssetCategory>());
        }
        
        //create category
        [HttpGet]
        [Route("create", Name = "AssetCategoryCreate")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(AssetCategory model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetAuthorizedClient();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/v1/AssetCategory/AddCategory", content);

            if (response.IsSuccessStatusCode)
                return RedirectToRoute("AssetCategoryIndex");

            ModelState.AddModelError("", "Unable to create category.");
            return View(model);
        }


        //get by assetcategory id
        [HttpGet]
        [Route("edit/{id}", Name = "AssetCategoryEdit")]
        public async Task<IActionResult> Edit(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/AssetCategory/GetCategoryById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<AssetCategory>(json);
                return View(category);
            }

            return RedirectToRoute("AssetCategoryIndex");
        }


        //Updating category
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, AssetCategory model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetAuthorizedClient();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/v1/AssetCategory/UpdateCategory/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToRoute("AssetCategoryIndex");

            ModelState.AddModelError("", "Unable to update category.");
            return View(model);
        }


        //deleting category
        [HttpGet]
        [Route("delete/{id}", Name = "AssetCategoryDelete")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/AssetCategory/GetCategoryById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<AssetCategory>(json);
                return View(category);
            }

            return RedirectToRoute("AssetCategoryIndex");
        }

        //confirm deletion
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}", Name = "AssetCategoryDeleteConfirmed")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.DeleteAsync($"api/v1/AssetCategory/DeleteByCategoryId/{id}");

            return RedirectToRoute("AssetCategoryIndex");
        }
    }
}
