# 🔐 Authorization Quick Reference

## User Roles in ShopEasy

### Administrator
- **Role:** `"Administrator"`
- **Created by:** Admin login at `/Auth/Login`
- **Credentials:** `admin@shopeasy.com / Admin@123`
- **Access:**
  - ✅ Admin Dashboard
  - ✅ Product management
  - ✅ Category management
  - ✅ Order management
  - ✅ Customer management

### Customer
- **Role:** `"Customer"`
- **Created by:** Registration at `/Account/Register` or login at `/Account/Login`
- **Access:**
  - ✅ Customer profile
  - ✅ View own orders
  - ❌ Admin pages

### Public User
- **Role:** None (not authenticated)
- **Access:**
  - ✅ Home page
  - ✅ Register page
  - ✅ Login pages
  - ❌ Protected pages

---

## URL Access Rules

| URL | Public | Customer | Admin |
|-----|--------|----------|-------|
| `/` | ✅ | ✅ | ✅ |
| `/Account/Register` | ✅ | ✅ | ✅ |
| `/Account/Login` | ✅ | ✅ | ✅ |
| `/Account/Profile` | ❌ | ✅ | ❌ |
| `/Auth/Login` | ✅ | ✅ | ✅ |
| `/Admin/Dashboard` | ❌ | ❌ | ✅ |
| `/Admin/Products/*` | ❌ | ❌ | ✅ |
| `/Admin/Categories/*` | ❌ | ❌ | ✅ |
| `/Admin/Orders/*` | ❌ | ❌ | ✅ |
| `/Admin/Customers/*` | ❌ | ❌ | ✅ |

---

## Authorization Attributes

```csharp
// No attribute - Public access
public class PublicModel : PageModel { }

// Require any authenticated user
[Authorize]
public class AuthenticatedModel : PageModel { }

// Require specific role
[Authorize(Roles = "Administrator")]
public class AdminOnlyModel : PageModel { }

// Require multiple roles
[Authorize(Roles = "Administrator,Moderator")]
public class MultiRoleModel : PageModel { }
```

---

## Login Flows

### Admin Login Flow
```
1. Visit /Auth/Login
2. Enter: admin@shopeasy.com / Admin@123
3. Receive claim: ClaimTypes.Role = "Administrator"
4. Can access: /Admin/*
```

### Customer Login Flow
```
1. Visit /Account/Register (create account)
   OR /Account/Login (existing account)
2. Enter: customer_email / password
3. Receive claim: ClaimTypes.Role = "Customer"
4. Can access: /Account/Profile
```

---

## Navigation Based on Role

The top navbar dynamically shows:

**If NOT logged in:**
```
Home | Privacy | 📝 Register | 🔓 Customer Login | 👨‍💼 Admin
```

**If logged in as Customer:**
```
Home | Privacy | 👤 My Profile | 🚪 Logout
```

**If logged in as Admin:**
```
Home | Privacy | 📊 Admin Dashboard | 🚪 Logout
```

---

## Troubleshooting Access Issues

### Can't access admin dashboard
Check: Are you logged in with `admin@shopeasy.com`?
- Yes → Check role in claims
- No → Login with admin credentials first

### Customer sees "Access Denied" on admin pages
This is correct! Customers shouldn't see admin pages.
- Navigate to `/Account/Profile` instead

### Logout button doesn't appear
You're not logged in. Use Register or Login buttons.

### Can't see Register/Login buttons
You're already logged in. Use navigation for your role.

---

## Session Management

- **Duration:** 30 days
- **Type:** Sliding expiration cookie
- **Logout:** Click "🚪 Logout" or cookie expires
- **Re-login:** Required after logout

---

## Security Best Practices

✅ **Do:**
- Use different emails for admin vs customer accounts
- Change admin password from default in production
- Use HTTPS in production
- Never share admin credentials
- Verify user role before granting access
- Log access attempts

❌ **Don't:**
- Hardcode credentials (use proper user management)
- Store passwords in plain text (use hashing)
- Skip authorization checks
- Trust client-side validation only
- Use admin account for shopping

---

## Code Examples

### Check if user is admin
```csharp
if (User.IsInRole("Administrator"))
{
	// User is admin
}
```

### Check if user is authenticated
```csharp
if (User.Identity.IsAuthenticated)
{
	// User is logged in (any role)
}
```

### Get current user info
```csharp
var name = User.FindFirst(ClaimTypes.Name)?.Value;
var email = User.FindFirst(ClaimTypes.Email)?.Value;
var role = User.FindFirst(ClaimTypes.Role)?.Value;
```

### Redirect based on role
```csharp
if (User.IsInRole("Administrator"))
	return RedirectToPage("/Admin/Dashboard");
else if (User.Identity.IsAuthenticated)
	return RedirectToPage("/Account/Profile");
else
	return RedirectToPage("/Account/Login");
```

---

## Files with Authorization

- `Pages/Admin/Dashboard.cshtml.cs` - `[Authorize(Roles = "Administrator")]`
- `Pages/Admin/Products/*.cshtml.cs` - `[Authorize(Roles = "Administrator")]`
- `Pages/Admin/Categories/*.cshtml.cs` - `[Authorize(Roles = "Administrator")]`
- `Pages/Admin/Orders/*.cshtml.cs` - `[Authorize(Roles = "Administrator")]`
- `Pages/Admin/Customers/*.cshtml.cs` - `[Authorize(Roles = "Administrator")]`
- `Pages/Account/Profile.cshtml.cs` - `[Authorize]`
- `Pages/Auth/Login.cshtml.cs` - (No attribute, public)

---

## Testing Commands

```powershell
# Run project
dotnet run

# Test as admin
# 1. Navigate to /Auth/Login
# 2. Enter: admin@shopeasy.com / Admin@123
# 3. Should see dashboard

# Test as customer
# 1. Navigate to /Account/Register
# 2. Create account
# 3. Login at /Account/Login
# 4. Should see profile page
```

---

**Last Updated:** 2026
**Status:** ✅ Fully Configured
