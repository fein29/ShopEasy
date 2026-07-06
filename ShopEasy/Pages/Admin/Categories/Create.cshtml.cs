using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopEasy.Models;

namespace ShopEasy.Pages.Admin.Categories
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly myContext _db;

        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(myContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Category = new Category();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Category.CreatedAt = DateTime.Now;
            _db.tbl_Category.Add(Category);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
