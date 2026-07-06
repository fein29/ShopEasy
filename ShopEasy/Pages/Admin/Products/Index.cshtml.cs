using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin.Products
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly myContext _db;
        public List<Product> Products { get; set; }

        public IndexModel(myContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            Products = await _db.tbl_Product
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
