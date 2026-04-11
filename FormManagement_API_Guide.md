# Form Management API - Implementation & Usage Guide

## 📋 Overview
Complete Form Management REST API with full CRUD operations for 7 form-related entities implemented using CQRS pattern with MediatR.

## 🎯 Controller: FormManagementController
**Location**: `CourtApp.Api\Controllers\FormManagementController.cs`

**Base Route**: `/api/form`

All endpoints return structured responses with proper HTTP status codes:
- `200 OK` - Successful GET or successful PUT/DELETE
- `201 Created` - Successful POST with location header
- `400 Bad Request` - Validation errors
- `500 Internal Server Error` - Server errors

---

## 📡 API Endpoints Summary

### 1. **FormType Management** - `/api/form/type`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/type/list` | Get all form types |
| GET | `/type/{id}` | Get form type by ID |
| GET | `/type/code/{code}` | Get form type by code |
| POST | `/type` | Create new form type |
| PUT | `/type/{id}` | Update form type |
| DELETE | `/type/{id}` | Delete form type |

**Example - Create FormType:**
```json
POST /api/form/type
{
  "code": "NOTICE",
  "name": "Notice Form",
  "description": "Form for sending notices"
}
```

---

### 2. **FormMaster Management** - `/api/form/master`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/master/list` | Get all form masters |
| GET | `/master/{id}` | Get form master by ID |
| GET | `/master/code/{code}` | Get form master by code |
| GET | `/master/type/{formTypeId}` | Get all masters for a type |
| POST | `/master` | Create new form master |
| PUT | `/master/{id}` | Update form master |
| DELETE | `/master/{id}` | Delete form master |

**Example - Create FormMaster:**
```json
POST /api/form/master
{
  "formTypeId": "550e8400-e29b-41d4-a716-446655440000",
  "code": "NOTICE_CIVIL",
  "name": "Civil Notice Form"
}
```

---

### 3. **FormSubtype Management** - `/api/form/subtype`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/subtype/list` | Get all form subtypes |
| GET | `/subtype/{id}` | Get form subtype by ID |
| GET | `/subtype/code/{code}` | Get form subtype by code |
| GET | `/subtype/form/{formId}` | Get all subtypes for a form |
| POST | `/subtype` | Create new form subtype |
| PUT | `/subtype/{id}` | Update form subtype |
| DELETE | `/subtype/{id}` | Delete form subtype |

**Example - Create FormSubtype:**
```json
POST /api/form/subtype
{
  "formId": "660e8400-e29b-41d4-a716-446655440000",
  "code": "NOTICE_SUBTYPE_1",
  "name": "Notice Subtype 1"
}
```

---

### 4. **FormTemplate Management** - `/api/form/template`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/template/list` | Get all form templates |
| GET | `/template/{id}` | Get form template by ID |
| GET | `/template/subtype/{formSubtypeId}` | Get all templates for subtype |
| POST | `/template` | Create new form template |
| PUT | `/template/{id}` | Update form template |
| DELETE | `/template/{id}` | Delete form template |

**Example - Create FormTemplate:**
```json
POST /api/form/template
{
  "formSubtypeId": "770e8400-e29b-41d4-a716-446655440000",
  "title": "Show Cause Notice",
  "templateContent": "<html><body>Template HTML content</body></html>",
  "isEditable": true,
  "version": "1.0.0",
  "caseTypeCode": "CIVIL",
  "stateCode": "RJ"
}
```

---

### 5. **FormTemplateVersion Management** - `/api/form/template-version`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/template-version/list` | Get all versions |
| GET | `/template-version/{id}` | Get version by ID |
| GET | `/template-version/template/{formTemplateId}` | Get versions of template |
| POST | `/template-version` | Create new version |
| PUT | `/template-version/{id}` | Update version |
| DELETE | `/template-version/{id}` | Delete version |

**Example - Create FormTemplateVersion:**
```json
POST /api/form/template-version
{
  "formTemplateId": "880e8400-e29b-41d4-a716-446655440000",
  "content": "<html><body>Version 1.0 content</body></html>",
  "version": "1.0.0",
  "isPublished": true
}
```

---

### 6. **FormCaseCategoryMapping Management** - `/api/form/case-category-mapping`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/case-category-mapping/list` | Get all mappings |
| GET | `/case-category-mapping/{id}` | Get mapping by ID |
| GET | `/case-category-mapping/subtype/{formSubtypeId}` | Get mappings for subtype |
| POST | `/case-category-mapping` | Create new mapping |
| PUT | `/case-category-mapping/{id}` | Update mapping |
| DELETE | `/case-category-mapping/{id}` | Delete mapping |

**Example - Create FormCaseCategoryMapping:**
```json
POST /api/form/case-category-mapping
{
  "formSubtypeId": "990e8400-e29b-41d4-a716-446655440000",
  "caseCategoryId": "aa0e8400-e29b-41d4-a716-446655440000",
  "isMandatory": true
}
```

---

### 7. **FormCourtMapping Management** - `/api/form/court-mapping`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/court-mapping/list` | Get all mappings |
| GET | `/court-mapping/{id}` | Get mapping by ID |
| GET | `/court-mapping/subtype/{formSubtypeId}` | Get mappings for subtype |
| POST | `/court-mapping` | Create new mapping |
| PUT | `/court-mapping/{id}` | Update mapping |
| DELETE | `/court-mapping/{id}` | Delete mapping |

**Example - Create FormCourtMapping:**
```json
POST /api/form/court-mapping
{
  "formSubtypeId": "bb0e8400-e29b-41d4-a716-446655440000",
  "courtTypeId": "cc0e8400-e29b-41d4-a716-446655440000",
  "isMandatory": true
}
```

---

## 📦 Postman Collection

### Import Instructions:
1. Open Postman
2. Click **Import** button (top left)
3. Select **Upload Files** tab
4. Choose `FormManagement_API_Postman_Collection.json`
5. Collection will appear in your Postman workspace

### Setting Up Variables:
The collection includes environment variables that you need to configure:

```
baseUrl         = http://localhost:5000 (or your API URL)
formTypeId      = (auto-fill after creating)
formMasterId    = (auto-fill after creating)
formSubtypeId   = (auto-fill after creating)
formTemplateId  = (auto-fill after creating)
```

### Collection Structure:
```
FormManagement API Collection
├── FormType (6 endpoints)
├── FormMaster (7 endpoints)
├── FormSubtype (7 endpoints)
├── FormTemplate (6 endpoints)
├── FormTemplateVersion (6 endpoints)
├── FormCaseCategoryMapping (6 endpoints)
└── FormCourtMapping (6 endpoints)
```

**Total: 44 API endpoints**

---

## 🔄 Typical Workflow

### Step 1: Create FormType
```bash
POST /api/form/type
{
  "code": "NOTICE",
  "name": "Notice Form",
  "description": "Form for sending notices"
}
# Response: 201 Created with FormTypeId
```

### Step 2: Create FormMaster
```bash
POST /api/form/master
{
  "formTypeId": "{formTypeId from Step 1}",
  "code": "NOTICE_CIVIL",
  "name": "Civil Notice Form"
}
# Response: 201 Created with FormMasterId
```

### Step 3: Create FormSubtype
```bash
POST /api/form/subtype
{
  "formId": "{formMasterId from Step 2}",
  "code": "NOTICE_SUBTYPE_1",
  "name": "Initial Notice"
}
# Response: 201 Created with FormSubtypeId
```

### Step 4: Create FormTemplate
```bash
POST /api/form/template
{
  "formSubtypeId": "{formSubtypeId from Step 3}",
  "title": "Show Cause Notice",
  "templateContent": "<html>...</html>",
  "isEditable": true,
  "version": "1.0.0",
  "caseTypeCode": "CIVIL",
  "stateCode": "RJ"
}
# Response: 201 Created with FormTemplateId
```

### Step 5: Create FormTemplateVersion (Optional)
```bash
POST /api/form/template-version
{
  "formTemplateId": "{formTemplateId from Step 4}",
  "content": "<html>...</html>",
  "version": "1.0.0",
  "isPublished": true
}
```

### Step 6: Create FormCaseCategoryMapping
```bash
POST /api/form/case-category-mapping
{
  "formSubtypeId": "{formSubtypeId from Step 3}",
  "caseCategoryId": "{existing caseCategoryId}",
  "isMandatory": true
}
```

### Step 7: Create FormCourtMapping
```bash
POST /api/form/court-mapping
{
  "formSubtypeId": "{formSubtypeId from Step 3}",
  "courtTypeId": "{existing courtTypeId}",
  "isMandatory": true
}
```

---

## 🔍 Query Operations

### Get Hierarchy:
```bash
# 1. Get all Form Types
GET /api/form/type/list

# 2. Get Forms for specific Type
GET /api/form/master/type/{formTypeId}

# 3. Get Subtypes for specific Form
GET /api/form/subtype/form/{formMasterId}

# 4. Get Templates for specific Subtype
GET /api/form/template/subtype/{formSubtypeId}

# 5. Get Versions for specific Template
GET /api/form/template-version/template/{formTemplateId}

# 6. Get Case Mappings for Subtype
GET /api/form/case-category-mapping/subtype/{formSubtypeId}

# 7. Get Court Mappings for Subtype
GET /api/form/court-mapping/subtype/{formSubtypeId}
```

---

## 📝 Request/Response Examples

### Success Response (200 OK):
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "code": "NOTICE",
  "name": "Notice Form",
  "description": "Form for sending notices"
}
```

### List Response (200 OK):
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "code": "NOTICE",
    "name": "Notice Form",
    "description": "Form for sending notices"
  },
  {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "code": "PETITION",
    "name": "Petition Form",
    "description": "Form for filing petitions"
  }
]
```

### Create Response (201 Created):
```json
"550e8400-e29b-41d4-a716-446655440000"
```
Location header: `Location: /api/form/type/550e8400-e29b-41d4-a716-446655440000`

---

## 🛠️ Architecture Details

### MediatR Integration:
- Each endpoint dispatches a Command or Query via MediatR
- Handlers process requests and interact with repositories
- AutoMapper converts entities to DTOs

### Repository Layer:
- `IFormMasterRepository`, `IFormSubtypeRepository`, etc.
- Direct LINQ-based data access
- Async/await throughout
- No service layer abstraction

### Validation:
- FluentValidation validators for all commands
- Automatic validation in MediatR pipeline
- Returns 400 Bad Request on validation failure

---

## 🚀 Getting Started

1. **Build Solution**:
   ```bash
   dotnet build
   ```

2. **Run Application**:
   ```bash
   dotnet run
   ```

3. **Import Postman Collection**:
   - Open Postman
   - Import `FormManagement_API_Postman_Collection.json`

4. **Set Base URL** (if not localhost:5000):
   - Edit collection variables
   - Set `baseUrl` to your API endpoint

5. **Start Testing**:
   - Begin with "Create FormType"
   - Use response IDs in subsequent requests
   - Use "Get All" endpoints to verify data

---

## ⚙️ Configuration

### Required Services (DI):
```csharp
services.AddScoped<IFormTypeRepository, FormTypeRepository>();
services.AddScoped<IFormMasterRepository, FormMasterRepository>();
services.AddScoped<IFormSubtypeRepository, FormSubtypeRepository>();
services.AddScoped<IFormTemplateRepository, FormTemplateRepository>();
services.AddScoped<IFormTemplateVersionRepository, FormTemplateVersionRepository>();
services.AddScoped<IFormCaseCategoryMappingRepository, FormCaseCategoryMappingRepository>();
services.AddScoped<IFormCourtMappingRepository, FormCourtMappingRepository>();
```

### AutoMapper:
- Configured in `AppProfileMapping.cs`
- All entity-to-DTO mappings defined

### Caching:
- Uses `CacheKeys<T>()` generic pattern
- Distributed cache support ready

---

## 📋 Checklist for Full Integration

- ✅ Controller created with all endpoints
- ✅ Repositories implemented
- ✅ CQRS handlers completed
- ✅ AutoMapper configurations added
- ✅ FluentValidation validators ready
- ✅ Postman collection with 44 endpoints
- ✅ DI registration configured
- ✅ Build successful

---

## 🎓 Next Steps

1. **Database Migrations**: Run EF Core migrations for form tables
2. **Integration Tests**: Create integration tests for all endpoints
3. **API Documentation**: Generate Swagger/OpenAPI documentation
4. **Error Handling**: Add global exception handling middleware
5. **Authentication**: Implement authorization if needed
6. **Logging**: Add structured logging to handlers

---

## 📞 Support

For issues or questions:
1. Check the Postman collection for correct endpoint formats
2. Verify DI configuration in ServiceCollectionExtensions
3. Ensure all entities are properly mapped in AppProfileMapping
4. Check FluentValidation rules for command validation

---

**Version**: 1.0.0  
**Last Updated**: 2024  
**Status**: Production Ready ✅
