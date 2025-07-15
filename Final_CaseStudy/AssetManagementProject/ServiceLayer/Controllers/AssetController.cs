using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepo _repo;
        private readonly ILogger<AssetController> _logger;

        public AssetController(IAssetRepo repo, ILogger<AssetController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllAssets")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAllAssets()
        {
            _logger.LogInformation("Fetching all assets...");
            var assets = await _repo.GetAllAssetsAsync();
            if (assets != null)
            {
                return Ok(assets); // 200
            }
            else
            {
                return NotFound(); //404
            }
        }

        [HttpGet]
        [Route("GetAssetById/{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAssetById(int id)
        {
            _logger.LogInformation("Fetching asset by id...");
            var asset = await _repo.GetAssetByIdAsync(id);
            if (asset == null) return NotFound();
            return Ok(asset);
        }

        [HttpPost]
        [Route("AddAsset")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsset([FromBody] Asset asset)
        {
            _logger.LogInformation("Adding asset...");
            var added = await _repo.AddAssetAsync(asset);
            if (added != null)
            {
                return Created(HttpContext.Request.Path, added); //201
                //return Ok(added);
            }
            else
            {
                return BadRequest();  //400
            }
        }

        [HttpPut]
        [Route("UpdateAsset")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsset([FromBody] Asset asset)
        {
            _logger.LogInformation("Updating asset...");
            var updated = await _repo.UpdateAssetAsync(asset);
            if (updated != null)
            {
                return Accepted(HttpContext.Request.Path, updated);
                //return Ok(updated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("DeleteAssetById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            _logger.LogInformation("Deleting asset...");
            var result = await _repo.DeleteAssetAsync(id);
            if (!result) return BadRequest();
            return Ok("Asset deleted successfully.");
        }

        [HttpGet]
        [Route("GetAssetByCategory/{categoryName}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAssetByCategory(int categoryId)
        {
            _logger.LogInformation("Fetching asset by category...");
            var list = await _repo.GetAssetsByCategoryAsync(categoryId);
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetAssetByStatus/{status}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAssetByStatus(string status)
        {
            _logger.LogInformation("Fetching asset by status...");
            var list = await _repo.GetAssetsByStatusAsync(status);
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetAssetByEmpId/{employeeId}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> GetAssetsByEmployeeId(int employeeId)
        {


            _logger.LogInformation("Fetching asset by emp id...");
            var assets = await _repo.GetAssetsByEmployeeIdAsync(employeeId);
            if (assets == null || !assets.Any())
                return NotFound("No assets assigned to this employee.");
            return Ok(assets);
        }
    }
}
