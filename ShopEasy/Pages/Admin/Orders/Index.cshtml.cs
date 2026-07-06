using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin.Orders
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly myContext _db;
        public List<Order> Orders { get; set; }

        public IndexModel(myContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            Orders = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
