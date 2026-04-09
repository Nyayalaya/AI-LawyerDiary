# Profile API - Quick Reference Guide

## 🚀 Quick Start

### Import Files into Postman
1. **Collection:** `PostmanCollection_ProfileAPI.json`
2. **Environment:** `Postman_ProfileAPI_Environment.json`

### Minimal Setup (2 Steps)
1. Get JWT Token from Auth endpoint
2. Replace `{{token}}` variable with your token

---

## 📋 API Endpoints Summary

| # | Method | Endpoint | Purpose |
|---|--------|----------|---------|
| **Profile Management** | | | |
| 1 | GET | `/api/v1/profile/{userId}` | Get user profile |
| 2 | PUT | `/api/v1/profile/{userId}` | Update profile |
| 3 | POST | `/api/v1/profile/{userId}/complete` | Complete initial profile |
| **Organization** | | | |
| 4 | POST | `/api/v1/profile/{userId}/organization` | Create organization |
| 5 | PUT | `/api/v1/profile/{userId}/organization-mapping` | Map user to organization |
| **Sub-User** | | | |
| 6 | POST | `/api/v1/profile/{userId}/subuser` | Create sub-user (Clerk/Associate) |
| **Billing** | | | |
| 7 | POST | `/api/v1/profile/{userId}/billing-info` | Add billing information |

---

## 🔐 Authentication

All endpoints require **Bearer Token** authentication:
```
Authorization: Bearer {{token}}
```

---

## 📝 Request/Response Examples

### Example 1: Get Profile
```
GET /api/v1/profile/user-123
```
**Response:** 200 OK
```json
{
  "statusCode": 200,
  "succeeded": true,
  "data": {
    "userId": "user-123",
    "firstName": "John",
    "lastName": "Doe"
  }
}
```

### Example 2: Create Organization
```
POST /api/v1/profile/user-123/organization
```
**Body:**
```json
{
  "organizationName": "Legal Associates",
  "registrationNumber": "REG-2024-001",
  "taxIdentificationNumber": "TIN-123456789"
}
```
**Response:** 201 Created
```json
{
  "statusCode": 201,
  "succeeded": true,
  "data": "550e8400-e29b-41d4-a716-446655440000"
}
```

### Example 3: Create Sub-User
```
POST /api/v1/profile/user-123/subuser
```
**Body:**
```json
{
  "firstName": "Rajesh",
  "lastName": "Kumar",
  "email": "rajesh@example.com",
  "phoneNumber": "+919876543211",
  "password": "SecurePassword@123",
  "role": "Clerk"
}
```
**Response:** 201 Created
```json
{
  "statusCode": 201,
  "succeeded": true,
  "data": "550e8400-e29b-41d4-a716-446655440001"
}
```

---

## 🎯 Common Status Codes

| Code | Meaning | Action |
|------|---------|--------|
| **200** | OK | Request successful |
| **201** | Created | Resource created |
| **400** | Bad Request | Invalid data - check request body |
| **401** | Unauthorized | Invalid/missing token |
| **404** | Not Found | Resource doesn't exist |
| **422** | Unprocessable Entity | Validation error - check error list |
| **500** | Server Error | Backend issue - contact support |

---

## ✅ Validation Rules

### Email
- Must be valid email format
- Must be unique in system

### Phone Number
- Format: `+?[1-9]\d{1,14}`
- Example: `+919876543210`

### Password (For Sub-Users)
- Minimum 8 characters recommended
- Include uppercase, lowercase, numbers

### Role Values
- **Lawyers:** "Lawyer"
- **Sub-users:** "Clerk" or "Associate"
- **Organization Roles:** "Admin", "Member", "Viewer"

### Gender Enum
- 0 = Male
- 1 = Female
- 2 = Other

### Address Type Enum
- 0 = Residential
- 1 = Official
- 2 = Other

---

## 🔄 Typical Workflow

```
1. Complete Profile
   ↓
2. Create Organization (optional)
   ↓
3. Update Organization Mapping (optional)
   ↓
4. Add Sub-Users (optional)
   ↓
5. Add Billing Information
   ↓
6. Get Profile (Verify All Data)
```

---

## 💡 Pro Tips

1. **Save Responses:** Use Postman's save response feature
2. **Use Variables:** Leverage `{{variable}}` syntax
3. **Reorder Tests:** Run profile endpoints in logical order
4. **Pre-request Scripts:** Automate token refresh
5. **Tests Tab:** Add assertions for validation

---

## 🐛 Troubleshooting

| Issue | Solution |
|-------|----------|
| 401 Unauthorized | Check if token is valid and not expired |
| 400 Bad Request | Validate all required fields are present |
| 404 Not Found | Verify user_id/org_id exists in system |
| 422 Validation Error | Check error messages in response |
| Connection Error | Ensure API is running on correct port |

---

## 📚 Additional Resources

- **Full Guide:** See `POSTMAN_TESTING_GUIDE.md`
- **Source Code:** Check Application/Features/Profile/
- **API Docs:** Available at `/swagger` endpoint

---

## 🎓 Learning Path

1. Start with **Get User Profile** (simplest)
2. Try **Update User Profile** (modify data)
3. Move to **Complete Profile** (comprehensive)
4. Test **Organization** endpoints
5. Create **Sub-Users**
6. Add **Billing Information**

---

**Ready to test?** Import the collection and environment files into Postman and follow the workflow above!
