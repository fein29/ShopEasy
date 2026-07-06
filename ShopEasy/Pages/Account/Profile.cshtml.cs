using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly myContext _db;
        public Customer Customer { get; set; }
        public int TotalOrders { get; set; }

        public ProfileModel(myContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            // Get the customer ID from claims
            var customerIdClaim = User.FindFirst("customer_Id");
            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
            {
                return;
            }

            // Load customer with orders
            Customer = await _db.tbl_Customer
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(c => c.customer_Id == customerId);

            if (Customer != null)
            {
                TotalOrders = Customer.Orders?.Count ?? 0;
            }
        }
    }
}
