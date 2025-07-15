using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Controllers;

namespace ServiceLayer.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AssetCategoryController : ControllerBase
    {
        private readonly IAssetCategoryRepo _repo;
        private readonly ILogger<AssetCategoryController> _logger;

        public AssetCategoryController(IAssetCategoryRepo repo,ILogger<AssetCategoryController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllCategory")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAllCategories()
        {
            _logger.LogInformation("Fetching all categories...");
            var categories = await _repo.GetAllAsync();
            if (categories != null)
            {
                return Ok(categories); // ok :200
            }
            else
            {
                return NotFound(); // 404
            }
        }

        [HttpGet]
        [Route("GetCategoryById/{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            _logger.LogInformation("Fetching category by id...");
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [Route("AddCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory([FromBody] AssetCategory category)
        {
            _logger.LogInformation("Adding category...");
            if (category != null)
            {
                var added = await _repo.AddAsync(category);
                return Created(HttpContext.Request.Path, added);
                //return Ok(added);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpdateCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id,[FromBody] AssetCategory category)
        {
            _logger.LogInformation("Updating category...");
            if (id != category.CategoryId)
                return BadRequest("ID in URL doesn't match body");
            var updated = await _repo.UpdateAsync(category);
            if(updated != null)
            {
                return Accepted(HttpContext.Request.Path,updated);
                //return Ok(updated);
            }
            else
            {
                return BadRequest();
            }
             
        }

        [HttpDelete]
        [Route("DeleteByCategoryId/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogInformation("Deleting category...");
            var result = await _repo.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok("Category deleted successfully.");
        }
    }

}
