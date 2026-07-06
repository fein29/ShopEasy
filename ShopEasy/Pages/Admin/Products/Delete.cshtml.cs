using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin.Products
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly myContext _db;

        [BindProperty]
        public Product Product { get; set; }

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

            Product = await _db.tbl_Product.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Product == null)
            {
                return NotFound();
            }

            var product = await _db.tbl_Product.FindAsync(Product.ProductId);
            if (product != null)
            {
                _db.tbl_Product.Remove(product);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
