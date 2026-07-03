using System.ComponentModel.DataAnnotations;

namespace ShopEasy.Models
{
    public class Product
    {
        [Key] 
        public int ProductId { get; set; }
        public string Product_Name { get; set; }
        public string Product_price { get; set; }
        public string Product_Description { get; set; }
        public string Product_image { get; set; }
        public int cat_id { get; set; }
         



    }
}
