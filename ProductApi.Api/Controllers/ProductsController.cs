using ECommerece.CommonLibrary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.IServices;
using ProductApi.Domain.Entities;

namespace ProductApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            IEnumerable<ProductDTO> productsList = new List<ProductDTO>();
            if (products.Any())
            {
                productsList = ProductConversion.MapFromEntity(products)!;
            }
            return productsList.Any() ? Ok(productsList) : NotFound("Products not found!");
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var productEntity = await _productService.GetByIdAsync(id);
            if (productEntity is null)
                return NotFound($"Product with id: {id} is not found");

            var product = ProductConversion.MapFromEntity(productEntity);
            return product is not null ? Ok(product) : NotFound($"Product with id: {id} is not found");
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> CreateProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productEntity = ProductConversion.MapIntoEntity(product);
            var createResponse = await _productService.CreateAsync(productEntity);

            return createResponse.IsSuccess ? Ok(createResponse) : BadRequest(createResponse);
        }
        [HttpPut]
        public async Task<ActionResult<BaseResponse>> UpdateProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productEntity = ProductConversion.MapIntoEntity(product);
            var updateResponse = await _productService.UpdateAsync(productEntity);

            return updateResponse.IsSuccess ? Ok(updateResponse) : BadRequest(updateResponse);
        }
        [HttpDelete]
        public async Task<ActionResult<BaseResponse>> DeleteProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productEntity = ProductConversion.MapIntoEntity(product);
            var deleteResponse = await _productService.DeleteAsync(productEntity);
            return deleteResponse.IsSuccess ? Ok(deleteResponse) : BadRequest(deleteResponse);
        }

        //[HttpGet]
        //public async Task<IActionResult> ExportProducts()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}

