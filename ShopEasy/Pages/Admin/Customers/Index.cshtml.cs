using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin.Customers
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly myContext _db;
        public List<Customer> Customers { get; set; }

        public IndexModel(myContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            Customers = await _db.tbl_Customer
                .Include(c => c.Orders)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
