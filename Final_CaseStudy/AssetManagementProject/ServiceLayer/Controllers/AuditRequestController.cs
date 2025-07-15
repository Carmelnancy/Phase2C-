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
    public class AuditRequestController : ControllerBase
    {
        private readonly IAuditRequestRepo _repo;
        private readonly ILogger<AuditRequestController> _logger;

        public AuditRequestController(IAuditRequestRepo repo,ILogger<AuditRequestController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        //  Admin - View all audit requests
        
        [HttpGet("GetAllAuditRequests")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAuditRequest()
        {
            _logger.LogInformation("Fetching all audit requests...");
            var requests = await _repo.GetAllRequestsAsync();
            if (requests != null)
            {
                return Ok(requests);
            }
            else
            {
                return NotFound();
            }
        }

        //  Employee - View their own audit requests
        [HttpGet("myrequests")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyRequests()
        {
            _logger.LogInformation("Fetching my audit request...");
            var employeeId = int.Parse(User.FindFirstValue("employeeId"));
            var list = await _repo.GetRequestsByEmployeeIdAsync(employeeId);
            return Ok(list);
        }

        //  Common - Get specific request by ID
        [HttpGet("GetByAuditId/{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetByAuditId(int id)
        {
            _logger.LogInformation("Fetching audit by audit id...");
            var request = await _repo.GetRequestByIdAsync(id);
            if (request == null) return NotFound();
            return Ok(request);
        }

        //  Admin - Raise audit request to employee
        [HttpPost]
        [Route("CreateAuditRequest")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAuditRequest([FromBody] AuditRequest request)
        {
            _logger.LogInformation("Create audit request...");
            if (request != null)
            {
                request.RequestDate = DateTime.Now;
                request.Status = "Pending";

                var added = await _repo.AddAuditRequestAsync(request);
                return Created(HttpContext.Request.Path,added);
            }
            else
            {
                return BadRequest();
            }
        }

        //  Employee - Update audit status (Verified / Rejected)
        [HttpPut("Verify/{id}/status")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> VerifyAuditRequest(int id, [FromQuery] string newStatus)
        {
            _logger.LogInformation("Updating /verifying audit request...");
            var updated = await _repo.UpdateRequestStatusAsync(id, newStatus);
            if (updated != null)
            {
                return Ok(updated);
            }
            else
            {
                return BadRequest();
            }
        }

        //  Admin - Optionally delete audit request  --------- may not need
        [HttpDelete("DeleteByAuditId/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeleteRequestAsync(id);
            if (!result) return NotFound();
            return Ok("Audit request deleted successfully.");
        }
    }
}
