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
    public class AssetRequestController : ControllerBase
    {
        private readonly IAssetRequestRepo _repo;
        private readonly IAssetRepo _assetRepo;
        private readonly ILogger<AssetRequestController> _logger;

        public AssetRequestController(IAssetRequestRepo repo,IAssetRepo assetRepo, ILogger<AssetRequestController> logger)
        {
            _repo = repo;
            _assetRepo = assetRepo;
            _logger = logger;
        }

        //  For Admin - View all asset requests
        [HttpGet]
        [Route("GetAllAssetRequests")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAssetRequests()
        {
            _logger.LogInformation("Fetching all asset requests...");
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

        //  For Employee - View their own asset requests
        [HttpGet("myrequests")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyRequests()
        {
            _logger.LogInformation("Fetching my asset requests...");
            var employeeId = int.Parse(User.FindFirstValue("employeeId"));
            var list = await _repo.GetRequestsByEmployeeIdAsync(employeeId);
            return Ok(list);
        }

        //  For Admin or Employee - Get single request by ID
        [HttpGet("GetAssetRequestById/{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAssetRequestById(int id)
        {
            _logger.LogInformation("Fetching asset request by id...");
            var request = await _repo.GetRequestByIdAsync(id);
            if (request == null) return NotFound();
            return Ok(request);
        }

        //  For Employee - Raise a new asset request
        [HttpPost]
        [Route("CreateAssetRequest")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> CreateAssetRequest([FromBody] AssetRequest request)
        {
            _logger.LogInformation("Adding asset request...");
            var empIdClaim = User.FindFirst("employeeId")?.Value;

            if (string.IsNullOrEmpty(empIdClaim))
                return Unauthorized("Employee ID claim missing");
            request.EmployeeId = int.Parse(empIdClaim);
            if (request != null)
            {
                request.RequestDate = DateTime.Now;
                request.Status = "Pending";
                var added = await _repo.AddAssetRequestAsync(request);
                return Created(HttpContext.Request.Path, added);
            }
            else
            {
                return BadRequest();
            }

        }

        //  For Admin - Approve / Reject request
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAssetRequestStatus(int id, [FromQuery] string newStatus)
        {
            _logger.LogInformation("Updating asset request status...");
            var updated = await _repo.UpdateRequestStatusAsync(id, newStatus);
            if(updated == null) return BadRequest();
            return Accepted(HttpContext.Request.Path,updated);
        }

        //  Optional - Admin can delete request if needed   ------------------------- may not need it
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeleteRequestAsync(id);
            if (!result) return NotFound();
            return Ok("Request deleted successfully.");
        }

        [HttpPut("returnAsset/{requestId}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ReturnAsset(int requestId)
        {
            _logger.LogInformation("Request to return asset...");
            var result = await _repo.ReturnAssetAsync(requestId);
            if (result.Contains("not found")) return NotFound(result);
            return Ok(result);
        }



        [HttpPut("approve/{requestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveRequest(int requestId)
        {
            _logger.LogInformation("Approving asset request...");
            var request = await _repo.GetRequestByIdAsync(requestId);
            if (request == null)
                return NotFound("Asset request not found");

            var asset = await _assetRepo.GetAssetByIdAsync(request.AssetId);
            if (asset == null || asset.Quantity <= 0)
                return BadRequest("Asset not available");

            // Update request status
            //request.Status = "Approved";
            await _repo.UpdateRequestStatusAsync(request.RequestId,"Approved");

            // Assign asset logic (optional: maintain a table or just log/acknowledge here)
            asset.Quantity -= 1;
            await _assetRepo.UpdateAssetAsync(asset);

            // Optionally log assignment (e.g., into a separate table if you have one)

            return Ok($"Asset {asset.AssetName} assigned to Employee {request.EmployeeId} and request approved.");
        }

    }
}
