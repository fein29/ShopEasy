using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;
using ShopEasy.Utilities;
using System.Security.Claims;

namespace ShopEasy.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly myContext _db;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LoginModel(myContext db)
        {
            _db = db;
        }

        public void OnGet(string returnUrl = null)
        {
            // If user is already authenticated, redirect to profile
            if (User.Identity.IsAuthenticated && !User.IsInRole("Administrator"))
            {
                RedirectToPage("/Account/Profile");
            }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "❌ Email and password are required.";
                return Page();
            }

            try
            {
                // Find customer by email
                var customer = await _db.tbl_Customer
                    .FirstOrDefaultAsync(c => c.customer_Email == Email);

                if (customer == null)
                {
                    ErrorMessage = "❌ Invalid email or password.";
                    return Page();
                }

                // Verify password
                if (!PasswordHelper.VerifyPassword(Password, customer.customer_Password))
                {
                    ErrorMessage = "❌ Invalid email or password.";
                    return Page();
                }

                // Create claims for the authenticated user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, customer.customer_Email),
                    new Claim(ClaimTypes.Name, customer.customer_Name),
                    new Claim(ClaimTypes.NameIdentifier, customer.customer_Id.ToString()),
                    new Claim("customer_Id", customer.customer_Id.ToString()),
                    new Claim(ClaimTypes.Role, "Customer")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(30)
                    });

                // Redirect to profile or return URL
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToPage("/Account/Profile");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"❌ Login error: {ex.Message}";
                return Page();
            }
        }
    }
}
