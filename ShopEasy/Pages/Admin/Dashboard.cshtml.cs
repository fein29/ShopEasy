using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class DashboardModel : PageModel
    {
        private readonly myContext _db;

        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }
        public List<Order> RecentOrders { get; set; }
        public List<Product> LowStockProducts { get; set; }

        public DashboardModel(myContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            try
            {
                TotalProducts = await _db.tbl_Product.CountAsync();
                TotalCategories = await _db.tbl_Category.CountAsync();
                TotalCustomers = await _db.tbl_Customer.CountAsync();
                TotalOrders = await _db.Orders.CountAsync();

                // Get recent orders (last 5)
                RecentOrders = await _db.Orders
                    .Include(o => o.Customer)
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .ToListAsync();

                // Get low stock products (stock < 10)
                LowStockProducts = await _db.tbl_Product
                    .Where(p => p.Stock < 10)
                    .OrderBy(p => p.Stock)
                    .Take(5)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in Dashboard: {ex.Message}");
            }
        }
    }
}
