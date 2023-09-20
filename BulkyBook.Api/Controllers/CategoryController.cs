using BulkyBook.Api.Model;
using BulkyBook.Api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{num}")]
        public async Task<IActionResult> Index(int num)
        {
            return Ok(await _categoryService.GetCategories(num));
            
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await (_categoryService.GetCategoryId(id)));
        }

        [HttpPost("Create")]
        [ProducesResponseType(statusCode: 201)]
        public async Task<IActionResult> Create([FromBody]Category category)
        {
            await _categoryService.Add(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Category category)
        {
            
            await _categoryService.Update(category);
            return NoContent();

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _categoryService.GetCategoryId(id);
            if (cat == null) { return  BadRequest(); }
            await _categoryService.Delete(cat);

            return NoContent();

        }










    }
}
