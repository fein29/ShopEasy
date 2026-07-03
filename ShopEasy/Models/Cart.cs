using System.ComponentModel.DataAnnotations;

namespace ShopEasy.Models
{
    public class Cart
    {
        [Key]
        public int cart_id { get; set; }
        public int ProductId { get; set; }
        public int customer_Id { get; set; }
        public int product_quantity { get; set; }
        public int cart_status { get; set; }






    }
}
