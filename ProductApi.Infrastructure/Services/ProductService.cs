using System.Linq.Expressions;
using ECommerece.CommonLibrary.Logs;
using ECommerece.CommonLibrary.Responses;
using Microsoft.EntityFrameworkCore;
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
                if (existProduct is not null && !string.IsNullOrEmpty(existProduct.Name))
                    return new BaseResponse(false, $"\"{product.Name}\" is already used as a product name");

                var newProduct = context.Product.Add(product).Entity;
                await context.SaveChangesAsync();

                if (newProduct is not null && newProduct.Id > 0)
                    return new BaseResponse(true, "Product has been added successfully");
                return new BaseResponse(false,$"Error accurred while saving {product.Name}");
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                return new BaseResponse(false, "Error accurred while adding new product");
            }
        }

        public async Task<BaseResponse> DeleteAsync(Product product)
        {
            try
            {
                var deletedProduct = await GetByIdAsync(product.Id);
                if (deletedProduct is null)
                    return new BaseResponse(false, "Product not found");
                context.Product.Remove(product);
                await context.SaveChangesAsync();

                return new BaseResponse(true, "Product has been deleted successfully");
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                return new BaseResponse(false, "Error accurred while deleting the product");
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                var products = await context.Product.AsNoTracking().ToListAsync();
                return products is not null ? products : null!;
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                throw new InvalidOperationException("Error occurred while retrieving the products");
            }
        }

        public async Task<Product> GetByAsync(Expression<Func<Product, bool>> predicate)
        {
            try
            {
                var product = await context.Product.FirstOrDefaultAsync(predicate);
                return product is not null ? product : null!;
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                throw new InvalidOperationException("Error accurred while retrieving the product");
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                var product = await context.Product.FirstOrDefaultAsync(p => p.Id == id);
                return product is not null ? product : null!;
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                throw new InvalidOperationException("Error accurred while retrieving the product");
            }
        }

        public async Task<BaseResponse> UpdateAsync(Product entity)
        {
            try
            {
                var exisit = await context.Product.AnyAsync(p => p.Id == entity.Id);
                if (!exisit)
                    return new BaseResponse(false, "Product not found");
                 
                context.Product.Update(entity);
                await context.SaveChangesAsync();
                return new BaseResponse(true, "Product has been updated successfully");
            }
            catch (Exception ex)
            {
                //Logging the error:
                LogException.LogExceptions(ex);
                //Return user friendly error respose:
                return new BaseResponse(false, "Error accurred while updateing the product");
            }

        }
    }
}
