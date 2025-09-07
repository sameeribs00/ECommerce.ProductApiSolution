using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
