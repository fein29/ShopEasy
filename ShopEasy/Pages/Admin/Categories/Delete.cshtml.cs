using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin.Categories
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly myContext _db;

        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(myContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _db.tbl_Category
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Category == null)
            {
                return NotFound();
            }

            var category = await _db.tbl_Category.FindAsync(Category.CategoryId);
            if (category != null)
            {
                _db.tbl_Category.Remove(category);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
