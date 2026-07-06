using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin.Categories
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly myContext _db;
        public List<Category> Categories { get; set; }

        public IndexModel(myContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            Categories = await _db.tbl_Category
                .Include(c => c.Products)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
