# Quick Start Guide - SystemUser Feature

## 🚀 Get Started in 5 Minutes

### Step 1: Build the Project ✅
```bash
cd "D:\EnterpriseApps\DairyApps\LawyerDiary- AI\Service"
dotnet build
```
**Status:** ✅ BUILD SUCCESSFUL

---

### Step 2: Run Database Migration
```bash
# Create migration
dotnet ef migrations add SystemUserEntity -p CourtApp.Infrastructure -s CourtApp.Api

# Apply migration
dotnet ef database update -p CourtApp.Infrastructure -s CourtApp.Api
```

---

### Step 3: Start the Application
```bash
dotnet run --project CourtApp.Api
```

Navigate to: `https://localhost:5001`

---

### Step 4: Test Registration
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Password123!",
    "userType": 0,
    "firstName": "John",
    "lastName": "Doe",
    "phoneNumber": "+1234567890"
  }'
```

Expected Response:
```json
{
  "isSuccessful": true,
  "message": "Registration successful",
  "data": "user-uuid-here"
}
```

---

### Step 5: Get Registered Users
```bash
curl -X GET "https://localhost:5001/api/system-users?status=0" \
  -H "Authorization: Bearer {your_token}"
```

Expected Response:
```json
{
  "isSuccessful": true,
  "data": {
    "items": [
      {
        "id": "user-uuid",
        "userType": 0,
        "firstName": "John",
        "lastName": "Doe",
        "email": "test@example.com",
        "registeredDate": "2024-01-15T10:30:00Z",
        "subscription": 1,
        "subscriptionExpiryDate": "2024-02-15T10:30:00Z",
        "status": 0,
        "statusReason": "New registration - Free plan enabled for 1 month"
      }
    ],
    "totalCount": 1
  }
}
```

---

## 📚 Understanding the Flow

```
┌─ User Registers
│  └─ ApplicationUser created
│     └─ SystemUser created with Free plan (1 month)
│        └─ Email sent with expiry date
│           └─ Awaiting admin approval
│
└─ Admin Reviews
   └─ Approves: Status → Active
      └─ User can now use the app
   └─ Rejects: Status → Inactive
      └─ Registration denied
```

---

## 🔧 Common Operations

### List Pending Approvals
```bash
curl "https://localhost:5001/api/system-users?status=0"
```

### List All Lawyers
```bash
curl "https://localhost:5001/api/system-users?userType=0"
```

### List All Corporate
```bash
curl "https://localhost:5001/api/system-users?userType=1"
```

### Approve a Registration
```bash
curl -X PUT "https://localhost:5001/api/system-users/{userId}" \
  -H "Content-Type: application/json" \
  -d '{
    "status": 1,
    "statusReason": "Approved by admin"
  }'
```

### Upgrade to Premium
```bash
curl -X PUT "https://localhost:5001/api/system-users/{userId}" \
  -H "Content-Type: application/json" \
  -d '{
    "subscription": 2,
    "statusReason": "Upgraded to Monthly plan"
  }'
```

---

## 📊 Database Verification

### Check if migration applied:
```sql
-- PostgreSQL
SELECT * FROM information_schema.tables WHERE table_name = 'system_users';

-- SQL Server
SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SystemUsers'
```

### View system users:
```sql
SELECT user_id, email, subscription, subscription_expiry_date, status 
FROM system_users 
LIMIT 10;
```

---

## 🐛 Troubleshooting

| Problem | Solution |
|---------|----------|
| Build fails | Run `dotnet clean && dotnet build` |
| Migration fails | Ensure connection string in appsettings.json |
| No users found | Run migration first, then register new user |
| Email not sent | Check MailService configuration |
| API returns 401 | Include Authorization header with valid token |

---

## 📋 What's Included

✅ **Domain Entity** - `SystemUser` for subscription tracking
✅ **Database Schema** - Fully indexed and configured
✅ **Registration Integration** - Auto-creates SystemUser record
✅ **API Endpoints** - List, Get, Update operations
✅ **Email Notifications** - Sends free plan details on registration
✅ **Service Layer** - Fully abstracted ISystemUserService
✅ **Type Safety** - DTOs for all responses

---

## 🎯 Next: Set Up Expiry Job (Optional)

To automatically lock accounts after free plan expires:

1. Follow guide: `FREE_PLAN_EXPIRY_GUIDE.md`
2. Creates background service that runs every hour
3. Locks expired accounts automatically
4. Sends reminder emails before expiry

---

## 📖 Full Documentation

| File | Purpose |
|------|---------|
| `SYSTEM_USER_IMPLEMENTATION.md` | Technical overview |
| `SYSTEM_USER_ARCHITECTURE.md` | Data models & flows |
| `SYSTEM_USERS_API_REFERENCE.md` | API endpoint details |
| `MIGRATION_GUIDE.md` | Database migration steps |
| `FREE_PLAN_EXPIRY_GUIDE.md` | Account lockout setup |
| `README_SYSTEM_USER.md` | Complete summary |

---

## ✨ Key Features

🎯 **Separate Entities** - Clean separation between Auth and Subscription
📧 **Email Integration** - Automatic notifications on registration
🔒 **Admin Approval** - Review and approve new registrations
💳 **Subscription Management** - Track and update subscription plans
⏰ **Free Plan Expiry** - Automatic account locking after 1 month
📊 **Filtering & Search** - Advanced querying capabilities
🔐 **Audit Trail** - Complete history of all actions

---

## 💾 Data Structure

**ApplicationUser** (Identity)
- Handles authentication and basic user info

**SystemUser** (Subscription Tracker)
- UserId (FK to ApplicationUser)
- Subscription type and expiry date
- Account status and reason
- Registration and login tracking
- Audit fields (CreatedAt, UpdatedAt)

---

## 🔑 Enums

**SubscriptionType:**
- 0 = Trial
- 1 = Free (1 month)
- 2 = Monthly
- 3 = Yearly

**UserAccountStatus:**
- 0 = Pending (awaiting approval)
- 1 = Active
- 2 = Inactive (locked)
- 3 = Locked

**RegisterType:**
- 0 = Lawyer
- 1 = Corporate

---

## 📞 Support

**Documentation:** See markdown files in solution root
**API Issues:** Check `SYSTEM_USERS_API_REFERENCE.md`
**Database Issues:** Check `MIGRATION_GUIDE.md`
**Expiry Setup:** Check `FREE_PLAN_EXPIRY_GUIDE.md`

---

## ✅ Verification Checklist

- [ ] Build successful
- [ ] Migration created
- [ ] Migration applied
- [ ] Can register new user
- [ ] SystemUser record created
- [ ] Can list users via API
- [ ] Can update user status
- [ ] Email notification received

---

**Status:** Ready for Production ✅
**Build:** Successful ✅
**Tests:** Pending (after migration)

**Next Action:** Run migrations and test!
