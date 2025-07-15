using System.Security.Claims;
using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestRepo _repo;
        private readonly ILogger<ServiceRequestController> _logger;

        public ServiceRequestController(IServiceRequestRepo repo, ILogger<ServiceRequestController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        //  Admin - View all service requests
        [HttpGet("Getallservicerequests")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllServiceRequests()
        {
            _logger.LogInformation("Fetching all service requests...");

            var requests = await _repo.GetAllRequestsAsync();
            if(requests == null) return NotFound();
            return Ok(requests);
        }

        //  Employee - View own service requests
        [HttpGet("myrequests")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyRequests()
        {
            _logger.LogInformation("Fetching my service request...");

            var employeeId = int.Parse(User.FindFirstValue("employeeId"));
            var list = await _repo.GetRequestsByEmployeeIdAsync(employeeId);
            return Ok(list);
        }

        //  Common - Get specific request by ID
        [HttpGet("GetByserviceId/{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetByServiceRequestId(int id)
        {
            _logger.LogInformation("Fetching service request by id...");

            var request = await _repo.GetRequestByIdAsync(id);
            if (request == null) return NotFound();
            return Ok(request);
        }

        //  Employee - can raise a service request
        [HttpPost]
        [Route("AddServiceRequest")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> AddServiveRequest([FromBody] ServiceRequest request)
        {
            _logger.LogInformation("Making service request...");

            request.RequestDate = DateTime.Now;
            request.Status = "Pending";

            var added = await _repo.AddServiceRequestAsync(request);
            return Ok(added);
        }

        //  Admin - Update status (approved/rejected)
        [HttpPut("UpdateStatus/{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] string newStatus)
        {
            _logger.LogInformation("Updating status of service request...");

            var updated = await _repo.UpdateRequestStatusAsync(id, newStatus);
            if (updated == null) return BadRequest();
            return Ok(updated);
        }

        //  Admin - Delete request (if needed)    ------- may not need
        [HttpDelete("DeleteServiceById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeleteRequestAsync(id);
            if (!result) return NotFound();
            return Ok("Request deleted successfully.");
        }
    }
}
