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
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllAsync();
            return new JsonResult(response);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _productService.GetByIdAsync(id);
            return new JsonResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productEntity = ProductConversion.MapIntoEntity(product);
            var response = await _productService.CreateAsync(productEntity);
            return new JsonResult(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productEntity = ProductConversion.MapIntoEntity(product);
            var response = await _productService.UpdateAsync(productEntity);
            return new JsonResult(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productEntity = ProductConversion.MapIntoEntity(product);
            var response = await _productService.DeleteAsync(productEntity);
            return new JsonResult(response);
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

