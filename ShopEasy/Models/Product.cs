using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopEasy.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_price { get; set; }
        public string Product_Description { get; set; }
        public string Product_image { get; set; }
        public int Stock { get; set; }
        public int cat_id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("cat_id")]
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
