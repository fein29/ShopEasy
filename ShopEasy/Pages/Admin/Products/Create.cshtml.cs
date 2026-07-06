using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin.Products
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly myContext _db;
        public List<Category> Categories { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        public CreateModel(myContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            Product = new Product();
            Categories = await _db.tbl_Category.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _db.tbl_Category.ToListAsync();
                return Page();
            }

            Product.CreatedAt = DateTime.Now;
            _db.tbl_Product.Add(Product);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
