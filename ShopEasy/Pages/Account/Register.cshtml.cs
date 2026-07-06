using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;
using ShopEasy.Utilities;

namespace ShopEasy.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly myContext _db;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public RegisterModel(myContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            // If user is already logged in, redirect to profile
            if (User.Identity.IsAuthenticated)
            {
                RedirectToPage("/Account/Profile");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || 
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "❌ Name, Email, and Password are required.";
                return Page();
            }

            // Check if passwords match
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "❌ Passwords do not match.";
                return Page();
            }

            // Validate password length
            if (Password.Length < 6)
            {
                ErrorMessage = "❌ Password must be at least 6 characters long.";
                return Page();
            }

            // Check if email already exists
            var existingCustomer = await _db.tbl_Customer
                .FirstOrDefaultAsync(c => c.customer_Email == Email);

            if (existingCustomer != null)
            {
                ErrorMessage = "❌ This email is already registered. Please <a href='/Account/Login'>login here</a> or use a different email.";
                return Page();
            }

            try
            {
                // Hash the password
                var hashedPassword = PasswordHelper.HashPassword(Password);

                // Create new customer with default values for optional fields
                var customer = new Customer
                {
                    customer_Name = Name,
                    customer_Email = Email,
                    customer_Phone = string.IsNullOrWhiteSpace(Phone) ? string.Empty : Phone,
                    customer_Address = string.IsNullOrWhiteSpace(Address) ? string.Empty : Address,
                    customer_Password = hashedPassword,
                    IsVerified = true,
                    CreatedAt = DateTime.Now
                };

                _db.tbl_Customer.Add(customer);
                await _db.SaveChangesAsync();

                SuccessMessage = $"✅ Account created successfully! Welcome, {Name}!";
                return Page();
            }
            catch (Exception ex)
            {
                // Log inner exception details for better debugging
                string errorDetails = ex.Message;
                if (ex.InnerException != null)
                {
                    errorDetails += " | " + ex.InnerException.Message;
                }
                ErrorMessage = $"❌ Error creating account: {errorDetails}";
                return Page();
            }
        }
    }
}
