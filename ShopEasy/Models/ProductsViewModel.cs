using ShopEasy.Models;

namespace ShopEasy.ViewModels
{
    public class ProductsViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public int? SelectedCategoryId { get; set; }
        public string SearchTerm { get; set; }
    }
}
