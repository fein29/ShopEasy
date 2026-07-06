using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin.Products
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly myContext _db;
        public List<Category> Categories { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        public EditModel(myContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _db.tbl_Product.FindAsync(id);
            Categories = await _db.tbl_Category.ToListAsync();

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _db.tbl_Category.ToListAsync();
                return Page();
            }

            _db.Attach(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating product: " + ex.Message);
                Categories = await _db.tbl_Category.ToListAsync();
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
