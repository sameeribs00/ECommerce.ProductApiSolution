using System.Linq.Expressions;
using ECommerece.CommonLibrary.Logs;
using ECommerece.CommonLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.DTOs;
using ProductApi.Application.IServices;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Data;

namespace ProductApi.Infrastructure.Services
{
    public class ProductService (ProductDbContext context): IProductService
    {
        public async Task<BaseResponse> CreateAsync(Product product)
        {
            try
            {
                var existProduct = await GetByAsync(p => p.Name!.Equals(product.Name));
                if (existProduct.IsSuccess && existProduct.Data is not null)
                    return new BaseResponse() { IsSuccess = false, Message = $"\"{product.Name}\" is already used as a product name" };

                var newProduct = context.Product.Add(product).Entity;
                await context.SaveChangesAsync();

                if (newProduct is not null && newProduct.Id > 0)
                    return new BaseResponse() { IsSuccess = true, Message = "Product has been added successfully" };
                return new BaseResponse() { IsSuccess = false, Message = $"Error accurred while saving {product.Name}" };
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                return new BaseResponse() { IsSuccess = false, Message = "Error accurred while adding new product" };
            }
        }

        public async Task<BaseResponse> DeleteAsync(Product product)
        {
            try
            {
                var deletedProduct = await context.Product.FindAsync(product.Id);
                if (deletedProduct is null)
                    return new BaseResponse() { IsSuccess = false, Message = "Product not found" };

                context.Entry(deletedProduct).State = EntityState.Detached;
                context.Product.Remove(product);
                await context.SaveChangesAsync();

                return new BaseResponse() { IsSuccess = true, Message = "Product has been deleted successfully" };
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                return new BaseResponse() { IsSuccess = false, Message = "Error accurred while deleting the product" };
            }
        }

        public async Task<BaseResponse> GetAllAsync()
        {
            try
            {
                var products = await context.Product.AsNoTracking().ToListAsync();
                var productDtos = products != null ? ProductConversion.MapFromEntity(products) : new List<ProductDTO>();
                return new BaseResponse() { IsSuccess = true, Data = productDtos };
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                return new BaseResponse() { IsSuccess = false, Message = "Error occurred while retrieving the products" };
            }
        }

        public async Task<BaseResponse> GetByAsync(Expression<Func<Product, bool>> predicate)
        {
            try
            {
                var product = await context.Product.FirstOrDefaultAsync(predicate);
                var productDto = product != null ? ProductConversion.MapFromEntity(product) : null;
                return new BaseResponse() { IsSuccess = true, Data = productDto };
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                return new BaseResponse() { IsSuccess = false, Message = "Error accurred while retrieving the product" };
            }
        }

        public async Task<BaseResponse> GetByIdAsync(int id)
        {
            try
            {
                var product = await context.Product.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                    return new BaseResponse() { IsSuccess = false, Message = $"No product detected with id: {id}" };

                var productDto = ProductConversion.MapFromEntity(product);
                return new BaseResponse() { IsSuccess = true, Data = productDto };
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                return new BaseResponse() { IsSuccess = false, Message = "Error accurred while retrieving the product" };
            }
        }

        public async Task<BaseResponse> UpdateAsync(Product entity)
        {
            try
            {
                var exisit = await context.Product.AnyAsync(p => p.Id == entity.Id);
                if (!exisit)
                    return new BaseResponse() { IsSuccess = false, Message = "Product not found" };
                 
                context.Product.Update(entity);
                await context.SaveChangesAsync();
                return new BaseResponse() { IsSuccess = true, Message = "Product has been updated successfully" };
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                return new BaseResponse() { IsSuccess = false, Message = "Error accurred while updateing the product" };
            }

        }
    }
}
