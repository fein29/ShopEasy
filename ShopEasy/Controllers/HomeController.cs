using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;
using System.Diagnostics;
using ShopEasy.ViewModels;

namespace ShopEasy.Controllers
{
    public class HomeController : Controller
    {
        private readonly myContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(myContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Get featured products (top 6 products with stock)
                var featuredProducts = await _db.tbl_Product
                    .Include(p => p.Category)
                    .Where(p => p.Stock > 0)
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(6)
                    .ToListAsync();

                // Get all categories
                var categories = await _db.tbl_Category
                    .ToListAsync();

                // Create view model with both categories and products
                var viewModel = new HomeViewModel
                {
                    Categories = categories,
                    FeaturedProducts = featuredProducts
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page");
                return View();
            }
        }

        public async Task<IActionResult> Products(int? categoryId = null, string searchTerm = null)
        {
            try
            {
                var query = _db.tbl_Product.Include(p => p.Category).AsQueryable();

                // Filter by category if specified
                if (categoryId.HasValue)
                {
                    query = query.Where(p => p.cat_id == categoryId.Value);
                }

                // Filter by search term if provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(p => p.Product_Name.Contains(searchTerm) || 
                                           p.Product_Description.Contains(searchTerm));
                }

                var products = await query
                    .OrderBy(p => p.Product_Name)
                    .ToListAsync();

                var categories = await _db.tbl_Category.ToListAsync();

                var viewModel = new ProductsViewModel
                {
                    Products = products,
                    Categories = categories,
                    SelectedCategoryId = categoryId,
                    SearchTerm = searchTerm
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading products");
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
