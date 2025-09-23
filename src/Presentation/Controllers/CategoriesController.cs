using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create([FromBody] CreationCategoryDTO creationCategoryDTO)
        {
            var createdCategory = await _categoryService.Create(creationCategoryDTO);
            return Created($"https://localhost:7283/categories/{createdCategory.Id}", createdCategory);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get([FromQuery] string? name)
        {
            return await _categoryService.GetAll(name);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById([FromRoute] int id)
        {
            return await _categoryService.GetById(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CreationCategoryDTO creationCategoryDTO)
        {
            await _categoryService.Update(id, creationCategoryDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _categoryService.Delete(id);
            return NoContent();
        }
    }
}
