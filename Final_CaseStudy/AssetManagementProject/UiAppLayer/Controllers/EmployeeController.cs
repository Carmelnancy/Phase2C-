using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UiAppLayer.Models;

namespace UiAppLayer.Controllers
{
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [NonAction]
        private HttpClient GetAuthorizedClient()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = _httpClientFactory.CreateClient("ApiClient"); //  Use named client

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        // view all employees
        [HttpGet("List", Name = "EmployeeList")]
        public async Task<IActionResult> List()
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync("api/v1/Employee/GetAllEmployees");

            if (!response.IsSuccessStatusCode)
                return View(new List<Employee>());

            var json = await response.Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<List<Employee>>(json);
            return View(employees);
        }

        // view details of employee by id
        [HttpGet("Details/{id}", Name = "EmployeeDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/Employee/GetEmployeeById/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var employee = JsonConvert.DeserializeObject<Employee>(json);

            // Get assets assigned to employee
            var assetResponse = await client.GetAsync($"api/v1/Asset/GetAssetByEmpid/{id}");
            List<Asset> assets = new List<Asset>();

            if (assetResponse.IsSuccessStatusCode)
            {
                var assetJson = await assetResponse.Content.ReadAsStringAsync();
                assets = JsonConvert.DeserializeObject<List<Asset>>(assetJson);
            }

            // Combine both into viewmodel
            var viewModel = new EmployeeDetailsViewModel
            {
                Employee = employee,
                Assets = assets
            };

            return View(viewModel);
            
        }

        //[HttpGet("Edit/{id}", Name = "EmployeeEdit")]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var client = GetAuthorizedClient();
        //    var response = await client.GetAsync($"api/v1/Employee/GetEmployeeById/{id}");

        //    if (!response.IsSuccessStatusCode)
        //        return RedirectToRoute("EmployeeList");

        //    var json = await response.Content.ReadAsStringAsync();
        //    var employee = JsonConvert.DeserializeObject<Employee>(json);
        //    return View(employee);
        //}

        //[HttpPost("Edit/{id}", Name = "EmployeeEditPost")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Employee employee)
        //{
        //    Console.WriteLine("Edit POST reached");
        //    ModelState.Remove("Password");
        //    ModelState.Remove("Role");
        //    if (!ModelState.IsValid)
        //    {
        //        foreach (var key in ModelState.Keys)
        //        {
        //            var errors = ModelState[key].Errors;
        //            foreach (var error in errors)
        //            {
        //                Console.WriteLine($"Field: {key}, Error: {error.ErrorMessage}");
        //            }
        //        }
        //        return View(employee);
        //    }

        //    var client = GetAuthorizedClient();
        //    //  Get existing employee to retain original password
        //    var existingRes = await client.GetAsync($"api/v1/Employee/GetEmployeeById/{id}");
        //    if (existingRes.IsSuccessStatusCode)
        //    {
        //        var existingJson = await existingRes.Content.ReadAsStringAsync();
        //        var existingEmp = JsonConvert.DeserializeObject<Employee>(existingJson);

        //        //  Retain old password
        //        employee.Password = existingEmp.Password;
        //        employee.Role = existingEmp.Role;
        //    }
        //    var json = JsonConvert.SerializeObject(employee);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await client.PutAsync("api/v1/Employee/UpdateEmployee", content);

         
        //    if (response.IsSuccessStatusCode)
        //    {
               
        //        return RedirectToRoute("EmployeeList");
        //    }


        //    ModelState.AddModelError("", "Failed to update employee.");
        //    return View(employee);
        //}


        // deleteing employee
        [HttpGet("Delete/{id}", Name = "EmployeeDelete")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.GetAsync($"api/v1/Employee/GetEmployeeById/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToRoute("EmployeeList");

            var json = await response.Content.ReadAsStringAsync();
            var employee = JsonConvert.DeserializeObject<Employee>(json);
            return View(employee);
        }

        // delete confirmation
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var client = GetAuthorizedClient();
            var response = await client.DeleteAsync($"api/v1/Employee/DeleteEmployeeById/{id}");

            return RedirectToRoute("EmployeeList");
        }
    }
}
