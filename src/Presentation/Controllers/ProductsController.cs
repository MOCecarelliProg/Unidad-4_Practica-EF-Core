using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create([FromBody] CreationProductDTO creationProductDto)
        {
            var createdProduct = await _productService.Create(creationProductDto);
            return Created($"https://localhost:7283/products/{createdProduct.Id}", createdProduct);
        }

        [HttpGet("whith-removed")]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            return await _productService.GetAll(null, null);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetNotRemoved([FromQuery] int? categoryId, [FromQuery] string? productName)
        {
            return await _productService.GetAll(categoryId, productName);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById([FromRoute] int id)
        {
            return await _productService.GetById(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CreationProductDTO creationProductDto)
        {
            await _productService.Update(id, creationProductDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _productService.Delete(id);
            return NoContent();
        }

        //[HttpGet]
        //public async Task<ActionResult<List<ProductDTO>>> GetByCategory([FromQuery] int categoryId)
        //{
        //    try
        //    {
        //        return await _productService.GetByCategory(categoryId);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet]
        //public async Task<ActionResult<List<ProductDTO>>> GetByName([FromQuery] string productName)
        //{
        //    try
        //    {
        //        return await _productService.GetByName(productName);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
