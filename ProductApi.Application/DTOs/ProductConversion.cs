using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs
{
    public static class ProductConversion
    {
        public static Product MapIntoEntity(ProductDTO product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity
        };
        public static ProductDTO MapFromEntity(Product product) => new(product.Id, product.Name!, product.Quantity, product.Price);
        public static IEnumerable<ProductDTO>? MapFromEntity(IEnumerable<Product>? products) => products!.Select(MapFromEntity).ToList();
       
    }
}
