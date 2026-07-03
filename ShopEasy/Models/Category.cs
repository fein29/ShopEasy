using System.ComponentModel.DataAnnotations;


namespace ShopEasy.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public int Category_name { get; set; }
    }
}
