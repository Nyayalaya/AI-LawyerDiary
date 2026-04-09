# Profile API - Visual Architecture & Workflow Diagrams

## 1. System Architecture Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│                        CLIENT (Postman)                         │
└────────────────────────┬────────────────────────────────────────┘
                         │ HTTP Request
                         ↓
┌─────────────────────────────────────────────────────────────────┐
│                       API Gateway Layer                         │
│                  (Base URL + Routing)                           │
└────────────────────────┬────────────────────────────────────────┘
                         │
                         ↓
┌─────────────────────────────────────────────────────────────────┐
│                    ProfileController                            │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │ - GetUserProfileAsync()         → Get profile           │   │
│  │ - UpdateProfileAsync()          → Update profile        │   │
│  │ - CompleteProfileAsync()        → Complete profile      │   │
│  │ - CreateOrganizationAsync()     → Create org            │   │
│  │ - CreateSubUserAsync()          → Create sub-user       │   │
│  │ - UpdateUserOrganizationAsync() → Map organization      │   │
│  │ - AddBillingInfoAsync()         → Add billing           │   │
│  └─────────────────────────────────────────────────────────┘   │
└────────────────────────┬────────────────────────────────────────┘
                         │
                         ↓
┌─────────────────────────────────────────────────────────────────┐
│                    MediatR Pipeline                             │
│              (Request-Response Mediator)                        │
└────────────────────────┬────────────────────────────────────────┘
         ┌──────────────┬─────────────────┬──────────────┐
         │              │                 │              │
         ↓              ↓                 ↓              ↓
    ┌────────┐    ┌──────────┐    ┌──────────┐    ┌─────────┐
    │Validator│   │Validator │   │Validator │   │Validator │
    │         │   │          │   │          │   │          │
    │FluentVal│   │FluentVal │   │FluentVal │   │FluentVal │
    └────────┘    └──────────┘    └──────────┘    └─────────┘
         │              │                 │              │
         └──────────────┴─────────────────┴──────────────┘
                         │
                         ↓
    ┌──────────────────────────────────────────────────┐
    │          Handler Layer (IRequestHandler)          │
    │  ┌──────────────────────────────────────────┐    │
    │  │- GetUserProfileQueryHandler              │    │
    │  │- UpdateProfileCommandHandler             │    │
    │  │- CompleteProfileCommandHandler           │    │
    │  │- CreateOrganizationCommandHandler        │    │
    │  │- CreateSubUserCommandHandler             │    │
    │  │- UpdateUserOrganizationCommandHandler    │    │
    │  │- AddUserBillingInfoCommandHandler        │    │
    │  └──────────────────────────────────────────┘    │
    └──────────────────┬───────────────────────────────┘
                       │
        ┌──────────────┼──────────────┐
        │              │              │
        ↓              ↓              ↓
┌──────────────┐ ┌──────────────┐ ┌──────────────┐
│IUserProfile  │ │IUserOrganiz  │ │IUserBilling  │
│Service       │ │ationService  │ │InfoService   │
└──────────────┘ └──────────────┘ └──────────────┘
        │              │              │
        └──────────────┼──────────────┘
                       │
                       ↓
         ┌─────────────────────────────┐
         │    Database Layer           │
         │  ┌───────────────────────┐  │
         │  │ User Profiles         │  │
         │  │ Organizations         │  │
         │  │ User Organizations    │  │
         │  │ Billing Info          │  │
         │  │ Addresses             │  │
         │  │ Contacts              │  │
         │  └───────────────────────┘  │
         └─────────────────────────────┘
```

---

## 2. Request-Response Flow Diagram

```
┌──────────────────────┐
│  Postman Request     │
│  GET /profile/{id}   │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│ ProfileController    │
│ GetUserProfileAsync  │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│ Create Query Object  │
│ GetUserProfileQuery  │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│ MediatR.Send()       │
│ Dispatch to Handler  │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│ Validator            │
│ Validate Request     │
│ ├─ User ID not empty │
│ └─ Valid format      │
└──────────┬───────────┘
           │
        ✓ Valid
           │
           ↓
┌──────────────────────┐
│ GetUserProfileQuery  │
│ Handler.Handle()     │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│ IUserProfileService  │
│ GetProfileAsync()    │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│ Database Query       │
│ Retrieve Profile     │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│ Check Result         │
│ ├─ Found: Return 200 │
│ └─ Not Found: 404    │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│ ApiResponse<T>       │
│ {                    │
│  statusCode: 200,    │
│  succeeded: true,    │
│  data: {...}         │
│ }                    │
└──────────┬───────────┘
           │
           ↓
┌──────────────────────┐
│ Return to Client     │
│ 200 OK Response      │
└──────────────────────┘
```

---

## 3. Complete Profile Workflow (User Journey)

```
START: New User Registration
  │
  ├─ User Creates Account (via Auth)
  │
  ├─ POST /profile/{userId}/complete
  │  ├─ Validate Personal Details
  │  │  ├─ First Name & Last Name (Required)
  │  │  ├─ Date of Birth (Required)
  │  │  └─ Gender (Required)
  │  │
  │  ├─ Add Addresses
  │  │  └─ At least 1 address (Required)
  │  │
  │  ├─ Add Contacts
  │  │  └─ At least 1 contact (Required)
  │  │
  │  ├─ Add Professional Info (Lawyers only)
  │  │  ├─ Bar Registration Number
  │  │  └─ Specializations
  │  │
  │  └─ Result: Profile Completed ✓
  │
  ├─ GET /profile/{userId}
  │  └─ Verify Complete Profile
  │
  ├─ Optional: Create Organization
  │  └─ POST /profile/{userId}/organization
  │     └─ Set as Organization Owner
  │
  ├─ Optional: Add Sub-Users
  │  └─ POST /profile/{userId}/subuser
  │     ├─ Create Clerk (Role)
  │     └─ Create Associate (Role)
  │
  ├─ Optional: Update Organization Mapping
  │  └─ PUT /profile/{userId}/organization-mapping
  │     └─ Map to Existing Organization
  │
  └─ POST /profile/{userId}/billing-info
     ├─ Add Banking Details
     ├─ Add GST Information
     └─ Profile Setup Complete! ✓
```

---

## 4. Error Handling Flow

```
┌────────────────────┐
│ Client Request     │
└────────┬───────────┘
         │
         ↓
┌────────────────────┐
│ Request Validation │
└────────┬───────────┘
         │
      ┌──┴──┐
      ↓     ↓
   [PASS] [FAIL]
      │     │
      │     ↓
      │  ┌──────────────────────┐
      │  │ Return 400/422 Error │
      │  │ {                    │
      │  │  statusCode: 400,    │
      │  │  succeeded: false,   │
      │  │  message: "...",     │
      │  │  errors: [...]       │
      │  │ }                    │
      │  └──────────────────────┘
      │
      ↓
┌────────────────────┐
│ Authentication     │
└────────┬───────────┘
         │
      ┌──┴──┐
      ↓     ↓
   [PASS] [FAIL]
      │     │
      │     ↓
      │  ┌──────────────────────┐
      │  │ Return 401 Error     │
      │  │ Missing/Invalid Token│
      │  └──────────────────────┘
      │
      ↓
┌────────────────────┐
│ Handler Execution  │
└────────┬───────────┘
         │
      ┌──┴──┐
      ↓     ↓
   [SUCCESS] [FAIL]
      │     │
      │     ↓
      │  ┌──────────────────────┐
      │  │ Return 404/500 Error │
      │  │ Resource not found   │
      │  │ or Server error      │
      │  └──────────────────────┘
      │
      ↓
┌────────────────────┐
│ Return 200/201     │
│ Success Response   │
└────────────────────┘
```

---

## 5. Data Model Relationships

```
┌──────────────────────────────────────┐
│         User Profile                 │
│ ┌────────────────────────────────┐   │
│ │ Id (PK)                        │   │
│ │ UserId (FK)                    │   │
│ │ FirstName                      │   │
│ │ LastName                       │   │
│ │ DateOfBirth                    │   │
│ │ Gender                         │   │
│ │ ProfileImageUrl                │   │
│ │ IsActive                       │   │
│ └────────────────────────────────┘   │
└────────────┬──────────────────────────┘
             │ 1:n
             ├─────────────────────────────────┐
             │                                 │
             ↓                                 ↓
    ┌─────────────────┐           ┌─────────────────┐
    │ Addresses       │           │ Contacts        │
    │ ├─ Id           │           │ ├─ Id           │
    │ ├─ UserId (FK)  │           │ ├─ UserId (FK)  │
    │ ├─ Address1     │           │ ├─ Type         │
    │ ├─ City         │           │ ├─ Email        │
    │ ├─ State        │           │ ├─ Phone        │
    │ └─ IsPrimary    │           │ └─ IsPrimary    │
    └─────────────────┘           └─────────────────┘

             │
             │ 1:1
             ↓
    ┌─────────────────────────┐
    │ Professional Info       │
    │ ├─ Id                   │
    │ ├─ UserId (FK)          │
    │ ├─ BarRegNumber         │
    │ ├─ Specializations      │
    │ └─ YearsOfExperience    │
    └─────────────────────────┘

             │
             │ 1:1
             ↓
    ┌─────────────────────────┐
    │ Billing Info            │
    │ ├─ Id                   │
    │ ├─ UserId (FK)          │
    │ ├─ BillingAddress       │
    │ ├─ AccountNumber        │
    │ ├─ PAN                  │
    │ └─ GSTNo                │
    └─────────────────────────┘

             │
             │ 1:n
             ↓
    ┌─────────────────────────┐
    │ User Organizations      │
    │ ├─ Id                   │
    │ ├─ UserId (FK)          │
    │ ├─ OrgId (FK)           │
    │ └─ Role                 │
    └─────────────────────────┘
             │
             │ n:1
             ↓
    ┌─────────────────────────┐
    │ Organizations           │
    │ ├─ Id                   │
    │ ├─ Name                 │
    │ ├─ RegNumber            │
    │ └─ OwnerUserId (FK)     │
    └─────────────────────────┘
```

---

## 6. API Response Status Code Tree

```
┌─────────────────────────────────────┐
│       HTTP Response Status          │
└────────────┬────────────────────────┘
             │
    ┌────────┴────────┐
    │                 │
    ↓                 ↓
┌──────────┐    ┌──────────────┐
│ Success  │    │ Client Error │
│ 2xx      │    │ 4xx          │
└────┬─────┘    └──────┬───────┘
     │                 │
     ├─ 200 OK        ├─ 400 Bad Request
     │  (GET,PUT)     │  (Missing/Invalid data)
     │                │
     ├─ 201 Created   ├─ 401 Unauthorized
     │  (POST)        │  (Missing/Invalid token)
     │                │
     │                ├─ 404 Not Found
     │                │  (Resource doesn't exist)
     │                │
     │                └─ 422 Unprocessable
     │                   (Validation error)
     │
     └─ 500 Server Error
        (Backend issue)
```

---

## 7. Command/Query Dispatch Pattern

```
HTTP Request
    │
    ↓
Controller Method
    │
    ├─ Validate Input Parameters
    │
    ├─ Create Command/Query Object
    │  ├─ GetUserProfileQuery
    │  ├─ UpdateProfileCommand
    │  ├─ CompleteProfileCommand
    │  ├─ CreateOrganizationCommand
    │  ├─ CreateSubUserCommand
    │  ├─ UpdateUserOrganizationCommand
    │  └─ AddUserBillingInfoCommand
    │
    ├─ Dispatch via MediatR
    │  └─ Mediator.Send(command)
    │
    ├─ Pipeline Execution:
    │  ├─ Validator runs
    │  │  └─ If invalid → Return error response
    │  │
    │  ├─ Handler runs
    │  │  ├─ Call service
    │  │  ├─ Get result from DB
    │  │  └─ Return result
    │  │
    │  └─ Result check
    │     ├─ Success → Return data with 200/201
    │     └─ Failure → Return error with message
    │
    ↓
HTTP Response
    ├─ Status Code (200/201/400/401/404/422/500)
    ├─ Body (ApiResponse<T>)
    └─ Headers (Content-Type, etc.)
```

---

## 8. Validation Pipeline

```
Input Request
    │
    ↓
┌─────────────────────────────────────┐
│ GetUserProfileQueryValidator        │
│ ├─ UserId NotEmpty ✓                │
│ └─ UserId NotNull ✓                 │
└─────────────────┬───────────────────┘
                  │
           ┌──────┴──────┐
           ↓             ↓
        PASS          FAIL
           │             │
           │             ↓
           │        ┌─────────────┐
           │        │ Error 422   │
           │        │ Validation  │
           │        │ failed      │
           │        └─────────────┘
           │
           ↓
┌─────────────────────────────────────┐
│ UpdateProfileCommandValidator       │
│ ├─ Profile NotNull ✓                │
│ ├─ UserId NotEmpty ✓                │
│ ├─ FirstName NotEmpty ✓             │
│ ├─ FirstName MaxLength(100) ✓       │
│ ├─ LastName NotEmpty ✓              │
│ ├─ PhoneNumber Format ✓             │
│ ├─ ProfileImageUrl URI ✓            │
│ └─ DateOfBirth LessThan(Today) ✓    │
└─────────────────┬───────────────────┘
                  │
           ┌──────┴──────┐
           ↓             ↓
        PASS          FAIL
           │             │
           │             ↓
           │        ┌─────────────┐
           │        │ Error 422   │
           │        │ List errors │
           │        └─────────────┘
           │
           ↓
PROCEED TO HANDLER
```

---

## 9. Postman Testing Flow

```
START
  │
  ├─ 1️⃣ Import Collection
  │  └─ PostmanCollection_ProfileAPI.json
  │
  ├─ 2️⃣ Import Environment
  │  └─ Postman_ProfileAPI_Environment.json
  │
  ├─ 3️⃣ Update Variables
  │  ├─ base_url
  │  ├─ token
  │  └─ user_id
  │
  ├─ 4️⃣ Test Get Profile
  │  └─ GET /profile/{userId}
  │
  ├─ 5️⃣ Test Update Profile
  │  └─ PUT /profile/{userId}
  │
  ├─ 6️⃣ Test Complete Profile
  │  └─ POST /profile/{userId}/complete
  │
  ├─ 7️⃣ Test Create Organization
  │  └─ POST /profile/{userId}/organization
  │
  ├─ 8️⃣ Test Organization Mapping
  │  └─ PUT /profile/{userId}/organization-mapping
  │
  ├─ 9️⃣ Test Create Sub-User
  │  └─ POST /profile/{userId}/subuser
  │
  ├─ 🔟 Test Add Billing Info
  │  └─ POST /profile/{userId}/billing-info
  │
  └─ ✅ Verify Get Profile
     └─ GET /profile/{userId} (Check all data)

END: All tests passed! ✓
```

---

## 10. Error Response Examples

```
┌───────────────────────────────────────────┐
│ 400 Bad Request                           │
├───────────────────────────────────────────┤
│ {                                         │
│   "statusCode": 400,                      │
│   "succeeded": false,                     │
│   "message": "User ID is required",       │
│   "errors": null                          │
│ }                                         │
└───────────────────────────────────────────┘

┌───────────────────────────────────────────┐
│ 401 Unauthorized                          │
├───────────────────────────────────────────┤
│ {                                         │
│   "statusCode": 401,                      │
│   "succeeded": false,                     │
│   "message": "Unauthorized",              │
│   "errors": null                          │
│ }                                         │
└───────────────────────────────────────────┘

┌───────────────────────────────────────────┐
│ 404 Not Found                             │
├───────────────────────────────────────────┤
│ {                                         │
│   "statusCode": 404,                      │
│   "succeeded": false,                     │
│   "message": "User profile not found",    │
│   "errors": null                          │
│ }                                         │
└───────────────────────────────────────────┘

┌───────────────────────────────────────────┐
│ 422 Unprocessable Entity                  │
├───────────────────────────────────────────┤
│ {                                         │
│   "statusCode": 422,                      │
│   "succeeded": false,                     │
│   "message": "Validation failed",         │
│   "errors": [                             │
│     "First name is required",             │
│     "Last name is required"               │
│   ]                                       │
│ }                                         │
└───────────────────────────────────────────┘
```

---

## Summary

These diagrams illustrate:
1. **System Architecture** - Complete layered structure
2. **Request-Response Flow** - How a request travels through the system
3. **User Workflow** - Complete profile setup journey
4. **Error Handling** - Decision tree for error scenarios
5. **Data Relationships** - Database model structure
6. **Status Codes** - HTTP response codes hierarchy
7. **CQRS Pattern** - Command/Query dispatch mechanism
8. **Validation** - Input validation pipeline
9. **Testing Flow** - Postman testing sequence
10. **Error Responses** - Example error responses

All diagrams are interconnected and show how different parts work together seamlessly!
