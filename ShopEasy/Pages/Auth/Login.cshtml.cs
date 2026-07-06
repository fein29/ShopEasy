using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ShopEasy.Pages.Auth
{
    public class LoginModel : PageModel
    {
        // Fixed admin credentials
        private const string ADMIN_EMAIL = "admin@shopeasy.com";
        private const string ADMIN_PASSWORD = "Admin@123";

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet(string returnUrl = null)
        {
            // If user is already authenticated, redirect to dashboard
            if (User.Identity.IsAuthenticated)
            {
                RedirectToPage("/Admin/Dashboard");
            }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // Verify credentials
            if (Email == ADMIN_EMAIL && Password == ADMIN_PASSWORD)
            {
                // Create claims for the authenticated user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Role, "Administrator")
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

                // Redirect to dashboard or return URL
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToPage("/Admin/Dashboard");
            }

            // Invalid credentials
            ErrorMessage = "❌ Invalid email or password. Please try again.";
            return Page();
        }
    }
}
