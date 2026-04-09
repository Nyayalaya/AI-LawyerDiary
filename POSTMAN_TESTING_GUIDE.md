# Profile API - Postman Testing Guide

## Overview
This document provides a complete guide to test the Profile Feature API using Postman. The Profile API includes endpoints for user profile management, organization management, sub-user management, and billing information.

## Setup Instructions

### 1. Import Postman Collection
1. Open Postman
2. Click on **Import** button (top-left)
3. Select the **PostmanCollection_ProfileAPI.json** file
4. The collection will be imported with all endpoints

### 2. Configure Environment Variables
Before testing, update the following variables in the collection:

```
base_url: http://localhost:5000  (or your API URL)
token: your_jwt_token_here        (obtained from authentication)
user_id: user-123                 (your test user ID)
```

To set variables in Postman:
1. Click on the **Environment** dropdown (top-right)
2. Create a new environment or select "Globals"
3. Add the variables mentioned above

## API Endpoints

### 1. Profile Management

#### 1.1 Get User Profile
- **Method:** GET
- **Endpoint:** `/api/v1/profile/{userId}`
- **Auth:** Required (Bearer Token)
- **Description:** Retrieve the complete profile information for a specific user
- **Response:** 200 OK - UserProfileDto

**Example Response:**
```json
{
  "statusCode": 200,
  "succeeded": true,
  "message": "Success",
  "data": {
    "userId": "user-123",
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com",
    "phoneNumber": "+919876543210",
    "isActive": true,
    "addresses": [],
    "contacts": [],
    "role": "Lawyer"
  }
}
```

---

#### 1.2 Update User Profile
- **Method:** PUT
- **Endpoint:** `/api/v1/profile/{userId}`
- **Auth:** Required (Bearer Token)
- **Description:** Update user profile information
- **Response:** 200 OK - Updated UserProfileDto

**Request Body:**
```json
{
  "firstName": "John",
  "middleName": "Kumar",
  "lastName": "Doe",
  "dateOfBirth": "1990-05-15",
  "gender": 0,
  "profileImageUrl": "https://example.com/profile.jpg",
  "phoneNumber": "+919876543210",
  "addresses": [
    {
      "addressLine1": "123 Main St",
      "addressLine2": "Apt 4B",
      "stateId": 1,
      "city": "Mumbai",
      "pincode": "400001",
      "type": 0,
      "isPrimary": true
    }
  ],
  "contacts": [
    {
      "contactType": 0,
      "email": "john.doe@example.com",
      "contactNumber": "+919876543210",
      "isPrimary": true
    }
  ],
  "professionalInfo": {
    "barRegistrationNumber": "BAR123456",
    "licenseExpiryDate": "2025-12-31",
    "specializations": ["Civil Law"],
    "yearsOfExperience": 10
  },
  "workLocations": [
    {
      "courtId": "court-123",
      "courtName": "Mumbai High Court",
      "isPrimary": true
    }
  ]
}
```

---

#### 1.3 Complete User Profile
- **Method:** POST
- **Endpoint:** `/api/v1/profile/{userId}/complete`
- **Auth:** Required (Bearer Token)
- **Description:** Complete user profile during initial setup
- **Response:** 201 Created - Completed UserProfileDto

**Request Body:**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "1990-05-15",
  "gender": 0,
  "profileImageUrl": "https://example.com/profile.jpg",
  "addresses": [
    {
      "addressLine1": "123 Main Street",
      "addressLine2": "Suite 100",
      "stateId": 1,
      "city": "Mumbai",
      "pincode": "400001",
      "type": 0,
      "isPrimary": true
    }
  ],
  "contacts": [
    {
      "contactType": 0,
      "email": "john.doe@example.com",
      "contactNumber": "+919876543210",
      "isPrimary": true
    }
  ],
  "professionalInfo": {
    "barRegistrationNumber": "BAR123456",
    "licenseExpiryDate": "2025-12-31",
    "specializations": ["Civil Law", "Criminal Law"],
    "yearsOfExperience": 10
  },
  "workLocations": [
    {
      "courtId": "court-123",
      "courtName": "Mumbai High Court",
      "isPrimary": true
    }
  ]
}
```

---

### 2. Organization Management

#### 2.1 Create Organization
- **Method:** POST
- **Endpoint:** `/api/v1/profile/{userId}/organization`
- **Auth:** Required (Bearer Token)
- **Description:** Create a new organization with the authenticated user as owner
- **Response:** 201 Created - Organization ID (GUID)

**Request Body:**
```json
{
  "organizationName": "Legal Associates Inc.",
  "registrationNumber": "REG-2024-001",
  "taxIdentificationNumber": "TIN-123456789"
}
```

**Success Response:**
```json
{
  "statusCode": 201,
  "succeeded": true,
  "message": "Created successfully",
  "data": "550e8400-e29b-41d4-a716-446655440000"
}
```

---

#### 2.2 Update User Organization Mapping
- **Method:** PUT
- **Endpoint:** `/api/v1/profile/{userId}/organization-mapping`
- **Auth:** Required (Bearer Token)
- **Description:** Update user's organization mapping and role
- **Response:** 200 OK - Boolean (true)

**Request Body:**
```json
{
  "organizationId": "org-123",
  "organizationName": "Legal Associates Inc.",
  "address": "123 Business Park, Mumbai",
  "role": "Admin"
}
```

**Success Response:**
```json
{
  "statusCode": 200,
  "succeeded": true,
  "message": "Success",
  "data": true
}
```

---

### 3. Sub-User Management

#### 3.1 Create Sub-User
- **Method:** POST
- **Endpoint:** `/api/v1/profile/{parentUserId}/subuser`
- **Auth:** Required (Bearer Token)
- **Description:** Create a new sub-user (Clerk or Associate) under the parent user
- **Response:** 201 Created - Sub-user ID (GUID)

**Request Body:**
```json
{
  "firstName": "Rajesh",
  "lastName": "Kumar",
  "email": "rajesh.kumar@example.com",
  "phoneNumber": "+919876543211",
  "password": "SecurePassword@123",
  "role": "Clerk"
}
```

**Validation Notes:**
- Role must be either "Clerk" or "Associate"
- Email must be valid and unique
- PhoneNumber must match international format: `+?[1-9]\d{1,14}`

---

### 4. Billing Information

#### 4.1 Add Billing Information
- **Method:** POST
- **Endpoint:** `/api/v1/profile/{userId}/billing-info`
- **Auth:** Required (Bearer Token)
- **Description:** Add or update billing information for the user
- **Response:** 201 Created - Boolean (true)

**Request Body:**
```json
{
  "billingName": "John Kumar Doe",
  "billingAddress": "123 Main Street, Suite 100",
  "city": "Mumbai",
  "state": "Maharashtra",
  "postalCode": "400001",
  "country": "India",
  "accountNumber": "1234567890123456",
  "ifscCode": "HDFC0000001",
  "branch": "Mumbai Branch",
  "pan": "ABCDE1234F",
  "gstNo": "27AABCR1234B1Z0"
}
```

**Success Response:**
```json
{
  "statusCode": 201,
  "succeeded": true,
  "message": "Created successfully",
  "data": true
}
```

---

## Field Validations

### Gender Enum Values
- 0 = Male
- 1 = Female
- 2 = Other

### Address Type Enum Values
- 0 = Residential
- 1 = Official
- 2 = Other

### Contact Type Enum Values
- 0 = Email
- 1 = Phone
- 2 = Mobile
- 3 = Fax

### Role Values
- **For Lawyers:** "Lawyer"
- **For Sub-users:** "Clerk" or "Associate"
- **For Organizations:** "Admin", "Member", or "Viewer"

---

## Testing Workflow

### Step 1: Create Complete Profile
1. First, call **Complete User Profile** endpoint
2. Provide all required information
3. Verify successful response (201 Created)

### Step 2: Update Profile
1. Call **Update User Profile** endpoint
2. Modify specific fields
3. Verify updated data (200 OK)

### Step 3: Create Organization
1. Call **Create Organization** endpoint
2. Note the organization ID
3. Use this ID in subsequent organization operations

### Step 4: Map User to Organization
1. Call **Update User Organization Mapping** endpoint
2. Assign role and organization details
3. Verify mapping (200 OK)

### Step 5: Create Sub-Users
1. Call **Create Sub-User** endpoint
2. Create Clerk and/or Associate accounts
3. Note the sub-user IDs for further operations

### Step 6: Add Billing Information
1. Call **Add Billing Information** endpoint
2. Provide complete billing details
3. Verify billing info saved (201 Created)

### Step 7: Retrieve and Verify
1. Call **Get User Profile** endpoint
2. Verify all information is correctly stored
3. Check nested objects (addresses, contacts, organization)

---

## Error Handling

### Common Error Responses

**400 Bad Request - Missing Required Field**
```json
{
  "statusCode": 400,
  "succeeded": false,
  "message": "User ID is required",
  "errors": null
}
```

**401 Unauthorized - Missing Token**
```json
{
  "statusCode": 401,
  "succeeded": false,
  "message": "Unauthorized",
  "errors": null
}
```

**404 Not Found - User Profile Not Found**
```json
{
  "statusCode": 404,
  "succeeded": false,
  "message": "User profile not found",
  "errors": null
}
```

**422 Unprocessable Entity - Validation Error**
```json
{
  "statusCode": 422,
  "succeeded": false,
  "message": "Validation failed",
  "errors": [
    "First name is required",
    "Last name is required"
  ]
}
```

---

## Tips for Testing

1. **Always Get Token First:** Ensure you have a valid JWT token from authentication before testing protected endpoints

2. **Use Collection Variables:** Leverage Postman collection variables for reusable values

3. **Check Response Status Codes:**
   - 200 = Successful GET/PUT
   - 201 = Successful POST (Created)
   - 400 = Bad Request (invalid data)
   - 401 = Unauthorized (missing/invalid token)
   - 404 = Not Found (resource doesn't exist)

4. **Validate Data Types:**
   - GUIDs should be valid UUID format
   - Dates should be ISO 8601 format (YYYY-MM-DD)
   - Email should be valid format
   - Phone should match international format

5. **Test Boundary Cases:**
   - Empty strings
   - Null values for optional fields
   - Maximum length strings
   - Invalid enum values

6. **Use Postman Tests:** Add test scripts in Postman to automate validation:
```javascript
pm.test("Status code is 200", function() {
  pm.response.to.have.status(200);
});

pm.test("Response has succeeded flag", function() {
  var jsonData = pm.response.json();
  pm.expect(jsonData.succeeded).to.be.true;
});
```

---

## Authentication Flow

1. **Login/Register:** Use Auth endpoints to get JWT token
2. **Copy Token:** Copy the JWT token from the response
3. **Set in Environment:** Paste token in Postman environment variables as `token`
4. **Automatic Bearer:** The collection uses Bearer authentication automatically

---

## Database Considerations

- **User ID:** Must be a valid user in the system
- **Organization ID:** Must exist before mapping
- **State ID:** Must reference valid state master data
- **Court ID:** Must reference valid court in the system

---

## Support & Documentation

For more details on the Profile Feature:
- Check the **Commands** folder for command definitions
- Check the **Handlers** folder for business logic
- Check the **Validators** folder for validation rules
- Review DTOs for required field structures

---

**Last Updated:** 2024
**API Version:** v1
**Collection Version:** 1.0
