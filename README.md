# 🏛️ LawyerDiary AI - Judicial Application Platform

# ✨ V2.0.0 with Complete API & Profile Feature - Ready! 🚀

# The Vision

An idea to bring together the best and essential practices / packages of ASP.NET Core 9.0 along with Clean Hexagonal Architecture that can be a right fit for small/mid and enterprise level solutions.
How easy would it be if you are able to run a single line of CLI command on your Console and you get a complete implementation in no time? That's the exact vision I have while building this full fledged Boilerplate template.

## 🎉 Latest Updates - V2.0.0

### ✅ Complete API Implementation
- **19 Controllers** - Fully implemented and documented
- **100+ Endpoints** - Ready for production use
- **Profile Feature** - Complete CQRS implementation with 7 endpoints
- **Complete Postman Collection** - All controllers and endpoints included
- **JWT Authentication** - Secure API access across all endpoints

### 📦 New Postman Collections Ready to Import!
- ✅ **`LawyerDiary_Complete_API_Collection.json`** ⭐ **MAIN COLLECTION**
  - 19 Complete Controllers
  - 100+ Fully functional endpoints
  - All CRUD operations
  - Ready for instant import to Postman

- ✅ **`PostmanCollection_ProfileAPI.json`** (Profile feature only)
- ✅ **`Postman_ProfileAPI_Environment.json`** (Pre-configured environment)

### 🎯 New Features
- ✅ Profile Management (Get, Update, Complete, Organization, Sub-users, Billing)
- ✅ Comprehensive Documentation (5 detailed guides)
- ✅ Visual Architecture Diagrams
- ✅ Testing Checklists & Guides
- ✅ Complete Postman Index Guide
- ✅ Auto-token management in Postman
- ✅ Pre-configured environment variables

## Technologies Used

- ✅ ASP.NET Core 9.0 WebAPI
- ✅ ASP.NET Core 9.0 MVC
- ✅ Entity Framework Core 9.0
- ✅ MediatR (CQRS Pattern)
- ✅ FluentValidation
- ✅ JWT Authentication
- ✅ AutoMapper
- ✅ Serilog Logging


## 🚀 Quick Start - Import API Collection to Postman

### ⭐ Best Option: Use Complete Collection

**Files Ready for Import:**
1. `LawyerDiary_Complete_API_Collection.json` - ALL 19 Controllers + 100+ Endpoints
2. `Postman_ProfileAPI_Environment.json` - Pre-configured variables

### 📥 Import Instructions (3 Steps)

**Step 1: Open Postman**
```
Launch Postman Application
```

**Step 2: Import Collection**
```
Click "Import" Button (Top-Left)
→ Select: LawyerDiary_Complete_API_Collection.json
→ 19 Folders with 100+ Endpoints Loaded!
```

**Step 3: Import Environment**
```
Click "Import" Button again
→ Select: Postman_ProfileAPI_Environment.json
→ Select Environment from dropdown
→ Ready to Use!
```

### ⚡ Test Immediately
```
POST /api/v1/auth/register
→ Create test user

POST /api/v1/auth/login
→ Token auto-saved to environment!

GET /api/v1/profile/{userId}
→ View your profile!
```

### 🎯 What You Get

**19 Complete Controller Collections:**
```
✓ Authentication (5 endpoints)
✓ Profile Management (7 endpoints)
✓ Location Management (6 endpoints)
✓ Court Management (5 endpoints)
✓ Court District (5 endpoints)
✓ Cadre Management (5 endpoints)
✓ Case Management (4 endpoints)
✓ Court Infrastructure (4 endpoints)
✓ Work & Proceeding (3 endpoints)
✓ State Management (1 endpoint)
... and more!
```

### 📖 Documentation
- See **POSTMAN_COLLECTIONS_INDEX.md** for detailed guide
- See **POSTMAN_TESTING_GUIDE.md** for comprehensive testing guide
- See **PROFILE_API_QUICK_REFERENCE.md** for quick reference

### In 3 Simple Steps:
1. **Download** `LawyerDiary_Complete_API_Collection.json`
2. **Open Postman** → Click Import → Select the JSON file
3. **Start Testing** - All 19 controllers, 100+ endpoints ready!

### Environment Setup:
```
base_url: http://localhost:5000 (or your API URL)
token: Your JWT token (auto-filled after login)
user_id: Your user ID (auto-filled after registration)
```

### First Request:
```
POST /api/v1/auth/register
  → Create a new user account

POST /api/v1/auth/login
  → Get JWT token (auto-saved to environment)

GET /api/v1/profile/{userId}
  → View your complete profile!
```

---

## Contributions / Help Needed

It would be great if a few of you could contribute to this project. Here are the points I would love to have some help with.

- Someone to fix typos on this Readme, or prepare a better one. 
- Someone to add Localizers throughout the MVC Project.
- Someone to add Arabic Transalations throughout the MVC Project. You can find the Dictionary under the Resources Folder in the Web Project.
- Someone to ensure the code quality.

Let's make this the best .NET 9 Clean Architecture Template with Complete API Implementation!

## Features Included

### 🎯 ASP.NET Core 9.0 MVC Project
- ✅ Slim Controllers using MediatR Library
- ✅ Permissions Management based on Role Claims
- ✅ Toast Notification (includes support for AJAX Calls too)
- ✅ Serilog
- ✅ ASP.NET Core Identity
- ✅ AdminLTE Bootstrap Template (Clean & SuperFast UI/UX)
- ✅ AJAX for CRUD (Blazing Fast load times)
- ✅ jQuery Datatables
- ✅ Select2
- ✅ Image Optimization
- ✅ Includes Sample CRUD Controllers / Views
- ✅ Active Route Tag Helper for UI
- ✅ RTL Support
- ✅ Complete Localization Support / Multilingual
- ✅ Clean Areas Implementation
- ✅ Dark Mode!
- ✅ Default Users / Roles Seeding at Startup
- ✅ Supports Audit Logging / Activity Logging for Entity Framework Core
- ✅ Automapper

### 🚀 ASP.NET Core 9.0 WebAPI - NEW!
- ✅ **19 Full Controllers** with CRUD operations
- ✅ **Authentication Controller** - Register, Login, Forgot Password, Reset Password
- ✅ **Profile Controller** - Complete user profile management
- ✅ **Location Controller** - Manage locations and districts
- ✅ **Court Management** - Courts, Districts, Complexes, Halls, Levels, Types
- ✅ **Case Management** - Categories, Stages, Types, Nature, Kind
- ✅ **Administrative** - Cadre, Work Masters, Proceeding Heads
- ✅ **100+ Endpoints** - Fully documented and tested
- ✅ **CQRS Pattern** - Clean command/query separation
- ✅ **FluentValidation** - Comprehensive input validation
- ✅ **JWT Authentication** - Secure API access
- ✅ **Standard Response Format** - Consistent API responses
- ✅ **Error Handling** - Proper HTTP status codes and error messages
- ✅ **Caching Support** - Improved performance with Redis-ready
- ✅ **Pagination** - Efficient data retrieval

### 📱 API Testing & Documentation
- ✅ **Complete Postman Collection** - All 19 controllers, 100+ endpoints
- ✅ **Ready to Import** - `LawyerDiary_Complete_API_Collection.json`
- ✅ **Auto-generated Environment** - Variables for easy testing
- ✅ **Sample Requests** - Pre-configured with examples
- ✅ **Authentication Flow** - Automatic token refresh
- ✅ **5 Comprehensive Guides** - Testing, quick reference, architecture diagrams

### ASP.NET Core 5.0 WebAPI
- JWT & Refresh Tokens
- Swagger



