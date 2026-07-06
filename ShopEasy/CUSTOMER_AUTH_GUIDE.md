# ShopEasy Customer Authentication - Complete Guide

## 🎯 Overview

ShopEasy now features a complete customer registration and login system alongside the admin authentication system.

---

## 🔑 Login/Register Access Points

When you run the project, you'll see these options in the **top navigation bar**:

### For New Visitors (Unauthenticated)
- **📝 Register** → `/Account/Register` - Create a new customer account
- **🔓 Customer Login** → `/Account/Login` - Login to existing account
- **👨‍💼 Admin** → `/Auth/Login` - Admin login (different from customer login)

### For Logged-In Customers
- **👤 My Profile** → `/Account/Profile` - View customer dashboard
- **🚪 Logout** → `/Account/Logout` - Sign out from session

### For Logged-In Admins
- **📊 Admin Dashboard** → `/Admin/Dashboard` - Admin management panel
- **🚪 Logout** → `/Account/Logout` - Sign out from session

---

## 📍 Complete URL Reference

### Customer Authentication Pages
| Page | URL | Purpose |
|------|-----|---------|
| Register Form | `/Account/Register` | Create new customer account |
| Login Form | `/Account/Login` | Login to customer account |
| Profile Dashboard | `/Account/Profile` | View profile & order history |
| Logout | `/Account/Logout` | Sign out |

### Admin Authentication Pages
| Page | URL | Purpose |
|------|-----|---------|
| Admin Login | `/Auth/Login` | Admin-only login |
| Admin Dashboard | `/Admin/Dashboard` | Main admin panel |
| Logout | `/Auth/Logout` | Admin sign out |

---

## 🚀 Quick Start Guide

### 1️⃣ **Register as a Customer**
1. Run the project (F5 in Visual Studio)
2. Click **📝 Register** in the top navigation
3. Fill in your details:
   - Full Name
   - Email Address
   - Phone Number (optional)
   - Address (optional)
   - Password (minimum 6 characters)
   - Confirm Password
4. Password strength indicator shows in real-time
5. Click **📝 Create Account**
6. You'll see a success message with a link to login

### 2️⃣ **Login to Your Account**
1. Click **🔓 Customer Login** in the navigation
2. Enter your email and password
3. Click **🔓 Login**
4. You'll be redirected to your profile dashboard

### 3️⃣ **View Your Profile**
1. After login, click **👤 My Profile**
2. See your account information:
   - Name, email, phone
   - Verification status
   - Member since date
   - Recent orders with status

### 4️⃣ **Logout**
1. Click **🚪 Logout** in the navigation
2. You'll be logged out and redirected to home

### 5️⃣ **Admin Access**
1. Click **👨‍💼 Admin** in the navigation (for unauthenticated users)
2. Login with:
   - **Email:** `admin@shopeasy.com`
   - **Password:** `Admin@123`
3. Access the admin dashboard and management panels

---

## 🔐 Password Requirements

✅ Minimum 6 characters
✅ Strength indicator shows:
- ❌ **Weak**: Less than 2 criteria met
- ⚠️ **Medium**: 2-3 criteria met
- ✅ **Strong**: 4+ criteria met

Criteria:
- At least 8 characters
- At least 12 characters
- Uppercase letters (A-Z)
- Lowercase letters (a-z)
- Numbers (0-9)
- Special characters (!@#$%^&*)

---

## 💾 Database Fields

### Customer Table Changes
Two new fields added to `tbl_Customer`:

```csharp
public string customer_Password { get; set; }  // SHA256 hashed password
public bool IsVerified { get; set; }          // Email verification status
```

---

## 🔒 Security Features

✅ **Password Hashing**
- Uses SHA256 algorithm
- Base64 encoded
- Never stored in plain text

✅ **Session Management**
- 30-day sliding expiration cookie
- Automatic logout after inactivity
- Secure cookie attributes

✅ **Data Validation**
- Email uniqueness check
- Password confirmation validation
- Client-side and server-side checks

✅ **Protected Pages**
- `/Account/Profile` requires `[Authorize]`
- Customer role properly distinguished from admin

---

## 📋 File Structure

```
ShopEasy/
├── Pages/
│   ├── Account/
│   │   ├── Register.cshtml          ← Registration form UI
│   │   ├── Register.cshtml.cs       ← Registration backend logic
│   │   ├── Login.cshtml             ← Customer login form
│   │   ├── Login.cshtml.cs          ← Customer login backend
│   │   ├── Profile.cshtml           ← Customer dashboard
│   │   ├── Profile.cshtml.cs        ← Profile backend
│   │   ├── Logout.cshtml.cs         ← Logout handler
│   │   └── Logout.cshtml
│   ├── Auth/
│   │   ├── Login.cshtml             ← Admin login (separate)
│   │   ├── Login.cshtml.cs
│   │   ├── Logout.cshtml.cs
│   │   └── Logout.cshtml
│   └── Shared/
│       └── _Layout.cshtml           ← Main layout with nav
├── Models/
│   ├── Customer.cs                  ← Customer model (updated)
│   └── myContext.cs                 ← DbContext
├── Utilities/
│   └── PasswordHelper.cs            ← SHA256 hashing utility
├── Views/
│   ├── Home/
│   │   └── Index.cshtml             ← Enhanced home page
│   └── Shared/
│       └── _Layout.cshtml
└── wwwroot/
	└── js/
		└── password-strength.js      ← Password strength checker
```

---

## ⚙️ Technical Implementation

### Authentication Scheme
- **Type:** Cookie-based (CookieAuthenticationDefaults)
- **Expiration:** 30 days with sliding expiration
- **Customer Login Path:** `/Account/Login`
- **Admin Login Path:** `/Auth/Login`
- **Logout Path:** `/Account/Logout`
- **Access Denied:** `/Auth/AccessDenied`

### Claims Used
```csharp
ClaimTypes.Email              // customer email
ClaimTypes.Name               // customer name
ClaimTypes.NameIdentifier     // customer ID
ClaimTypes.Role               // "Customer" or "Administrator"
```

### Password Hashing
```csharp
// Hash creation
string hashed = PasswordHelper.HashPassword(password);
// Binary -> Base64

// Verification
bool isValid = PasswordHelper.VerifyPassword(inputPassword, storedHash);
```

---

## ⚠️ Next Steps (Before Production)

1. **Run Database Migration**
   ```powershell
   Add-Migration AddCustomerAuth
   Update-Database
   ```

2. **Implement Email Verification**
   - Verify email before allowing orders
   - Send confirmation link

3. **Add Password Recovery**
   - "Forgot Password" functionality
   - Email-based reset

4. **Enhance Security**
   - Use HTTPS in production
   - Implement rate limiting on login
   - Add 2FA for sensitive operations

5. **Add User Profile Editing**
   - Allow customers to update info
   - Change password functionality

6. **Implement Order History**
   - Filter orders by date
   - Track order statistics

---

## 🐛 Testing Checklist

- [ ] Register new customer account
- [ ] Login with registered email/password
- [ ] View customer profile
- [ ] Logout and verify session cleared
- [ ] Test password validation (too short, weak strength)
- [ ] Test duplicate email prevention
- [ ] Try incorrect password (should fail)
- [ ] Admin login still works separately
- [ ] Protected pages redirect to login when not authenticated
- [ ] Navigation shows correct buttons based on auth status

---

## 📞 Support

For issues or questions about the authentication system, refer to the implementation files or contact the development team.

Last Updated: 2026
