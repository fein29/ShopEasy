# ShopEasy Admin Authentication - Quick Reference

## 🔐 Login Credentials

**Email:** `admin@shopeasy.com`
**Password:** `Admin@123`

## 📍 Access Points

- **Login Page:** `/Auth/Login`
- **Admin Dashboard:** `/Admin/Dashboard`
- **Logout:** `/Auth/Logout`

## 🔒 Protected Pages

The following admin pages are now protected with `[Authorize]` attribute:
- `/Admin/Dashboard` - Main dashboard
- `/Admin/Products/*` - All product management pages
- `/Admin/Categories/*` - All category management pages
- `/Admin/Orders/*` - All order management pages
- `/Admin/Customers/*` - All customer management pages

## 📋 Features Implemented

✅ Cookie-based authentication
✅ Fixed admin credentials
✅ Login form with validation
✅ Automatic redirect to login for unauthenticated users
✅ Logout functionality
✅ Session persistence (30 days)
✅ Professional login UI

## 🚀 How It Works

1. User visits `/Auth/Login`
2. User enters email and password
3. System validates against fixed credentials:
   - admin@shopeasy.com / Admin@123
4. On success:
   - User is authenticated
   - Cookie is set
   - User is redirected to `/Admin/Dashboard`
5. All admin pages check for authentication
6. Unauthenticated users are redirected to login
7. Click "Logout" to sign out

## 💻 Technical Details

- **Authentication Scheme:** Cookie-based (CookieAuthenticationDefaults)
- **Cookie Expiration:** 30 days with sliding expiration
- **Login Path:** `/Auth/Login`
- **Logout Path:** `/Auth/Logout`
- **Access Denied Path:** `/Auth/AccessDenied`

## 🔧 To Change Admin Credentials

Edit: `ShopEasy\Pages\Auth\Login.cshtml.cs`

```csharp
private const string ADMIN_EMAIL = "admin@shopeasy.com";
private const string ADMIN_PASSWORD = "Admin@123";
```

Change the constants to your desired credentials.

## ⚠️ Important Notes

- Credentials are hardcoded for simplicity
- In production, use proper user management and hashing
- Store credentials in secure configuration (not hardcoded)
- Consider implementing role-based access control (RBAC)
- Use HTTPS in production

