# Client & CaseDetail Controllers - Quick Reference

## Controllers Created

### 1. **ClientController** 
Location: `CourtApp.Api/Controllers/ClientController.cs`

**Base URL:** `api/v1/client`

#### Endpoints

| Method | Endpoint | Description | Returns |
|--------|----------|-------------|---------|
| POST | `/` | Create new client | `Result<Guid>` (201) |
| GET | `/{id}` | Get client by ID | `Result<ClientResponseDto>` |
| GET | `/` | Get all clients | `Result<List<ClientListDto>>` |
| PUT | `/{id}` | Update client | `Result<bool>` |
| DELETE | `/{id}` | Delete client | `Result<bool>` |
| GET | `/search` | Search clients (paginated) | `Result<PaginatedResult<ClientListDto>>` |

---

### 2. **CaseDetailController**
Location: `CourtApp.Api/Controllers/CaseDetailController.cs`

**Base URL:** `api/v1/casedetail`

#### Endpoints

| Method | Endpoint | Description | Returns |
|--------|----------|-------------|---------|
| POST | `/` | Create new case | `Result<Guid>` (201) |
| GET | `/{id}` | Get case by ID | `Result<UserCaseDetailResponse>` |
| PUT | `/{id}` | Update case | `Result<Guid>` |
| DELETE | `/{id}` | Delete case | `Result<Guid>` |
| GET | `/{id}/history` | Get case history | `Result<CaseHistoryResponse>` |
| GET | `/{id}/info` | Get case info | `Result<PaginatedResult<GetCaseInfoDto>>` |
| POST | `/{id}/documents` | Create case document | `Result<Guid>` (201) |
| GET | `/{id}/documents` | Get case documents | `Result<CaseDocumentResponse>` |

---

## Postman Collection

**File:** `CourtApp.Api/Postman_Collection_ClientAndCaseDetail.json`

### How to Import
1. Open Postman
2. Click **Import** button
3. Choose **Upload Files**
4. Select `Postman_Collection_ClientAndCaseDetail.json`
5. Update the variables (base_url, jwt_token, etc.)

### Variables to Set

```javascript
{
  "base_url": "http://localhost:5000",
  "jwt_token": "your_jwt_token_here",
  "client_id": "guid_of_client",
  "case_id": "guid_of_case"
}
```

---

## Usage Examples

### Client Operations

#### Create Client
```bash
curl -X POST "http://localhost:5000/api/v1/client" \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe",
    "email": "john@example.com",
    "mobile": "9876543210",
    "clientType": "Individual"
  }'
```

#### Get Client by ID
```bash
curl -X GET "http://localhost:5000/api/v1/client/{client_id}" \
  -H "Authorization: Bearer {token}"
```

#### Search Clients (with pagination)
```bash
curl -X GET "http://localhost:5000/api/v1/client/search?searchTerm=john&pageNumber=1&pageSize=10" \
  -H "Authorization: Bearer {token}"
```

---

### Case Operations

#### Create Case
```bash
curl -X POST "http://localhost:5000/api/v1/casedetail" \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{
    "institutionDate": "2024-01-15T00:00:00Z",
    "stateId": 1,
    "courtTypeId": "00000000-0000-0000-0000-000000000000",
    "caseCategoryId": "00000000-0000-0000-0000-000000000000",
    "caseTypeId": "00000000-0000-0000-0000-000000000000",
    "caseNo": "CASE-2024-001",
    "caseYear": 2024
  }'
```

#### Get Case by ID
```bash
curl -X GET "http://localhost:5000/api/v1/casedetail/{case_id}" \
  -H "Authorization: Bearer {token}"
```

#### Get Case History
```bash
curl -X GET "http://localhost:5000/api/v1/casedetail/{case_id}/history" \
  -H "Authorization: Bearer {token}"
```

---

## Response Format

### Success Response
```json
{
  "succeeded": true,
  "data": {
    "id": "uuid",
    "name": "John Doe",
    ...
  },
  "message": "Success",
  "statusCode": 200
}
```

### Paginated Response
```json
{
  "succeeded": true,
  "data": [
    { "id": "uuid", "name": "Item 1" },
    { "id": "uuid", "name": "Item 2" }
  ],
  "pagination": {
    "pageNumber": 1,
    "pageSize": 10,
    "totalCount": 50,
    "totalPages": 5
  },
  "statusCode": 200
}
```

### Error Response
```json
{
  "succeeded": false,
  "message": "Client not found",
  "statusCode": 404
}
```

---

## Authentication

All endpoints require JWT Bearer token in header:

```
Authorization: Bearer {jwt_token}
```

---

## Request/Response Validation

### Validation Rules

#### Create Client (Required Fields)
- Name: 2-255 characters
- Email: Valid email format
- Mobile: 10 digits
- ClientType: Required

#### Update Client (Required Fields)
- Name: 2-255 characters
- Email: Valid email format
- Mobile: 10 digits
- ClientType: Required

---

## Pagination Parameters

Use for both `GetAll` and `Search` endpoints:

```
pageNumber: int (default: 1)    // Must be > 0
pageSize: int (default: 10)     // Must be 1-100
searchTerm: string (optional)   // For search endpoint only
```

---

## Error Codes

| Code | Description |
|------|-------------|
| 200 | OK - Successful GET/PUT/DELETE |
| 201 | Created - Successful POST |
| 400 | Bad Request - Invalid input |
| 401 | Unauthorized - Missing/invalid token |
| 404 | Not Found - Resource doesn't exist |
| 422 | Validation Error - Invalid data |
| 500 | Server Error |

---

## Frontend Integration Examples

### Vue.js
```javascript
async createClient(formData) {
  try {
    const response = await this.$axios.post('/api/v1/client', formData, {
      headers: { Authorization: `Bearer ${this.token}` }
    });
    return response.data.data; // Returns client ID
  } catch (error) {
    console.error('Client creation failed:', error.response.data);
  }
}
```

### React
```javascript
const createClient = async (clientData) => {
  const response = await fetch('/api/v1/client', {
    method: 'POST',
    headers: {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(clientData)
  });
  
  const data = await response.json();
  return data.data;
};
```

### Angular
```typescript
createClient(clientData): Observable<ApiResponse<Guid>> {
  return this.http.post<ApiResponse<Guid>>(
    '/api/v1/client',
    clientData,
    { headers: this.getAuthHeaders() }
  );
}
```

---

## Testing Checklist

- [ ] Create client with valid data
- [ ] Create client with invalid email (should fail)
- [ ] Create client with short name (should fail)
- [ ] Get created client by ID
- [ ] Update client with new data
- [ ] Search clients by name
- [ ] Delete client
- [ ] Verify deleted client returns 404
- [ ] Create case
- [ ] Get case by ID
- [ ] Get case history
- [ ] Create case document
- [ ] Get case documents

---

## Common Issues & Solutions

### Issue: "Unauthorized" (401)
**Solution:** Ensure jwt_token is set and valid in Postman variables

### Issue: "Validation failed" (422)
**Solution:** Check required fields are present and valid
- Name must be 2-255 characters
- Email must be valid format (name@domain.com)
- Mobile must be exactly 10 digits

### Issue: "Not found" (404)
**Solution:** Verify the resource ID exists
- Use Get All or Search to list resources
- Copy correct ID from response

### Issue: "Bad request" (400)
**Solution:** Check JSON body formatting
- Ensure proper JSON syntax
- Verify all required fields are included
- Check data types (date formats, GUIDs, etc.)

---

## Performance Tips

1. **Use Pagination**
   - Load data in pages (default 10 per page)
   - Don't fetch all records at once

2. **Search Efficiently**
   - Use server-side search to filter results
   - Debounce search input on frontend (300ms)

3. **Cache Data**
   - Cache client list in frontend
   - Invalidate on create/update/delete

4. **Lazy Load**
   - Load case documents only when needed
   - Defer heavy operations

---

## API Specification

Full OpenAPI/Swagger documentation available at:
```
http://localhost:5000/swagger/index.html
```

---

## Support

For detailed feature documentation:
- ClientController: See `CLIENTS_FEATURE_README.md`
- CaseDetails: See existing CaseDetail documentation
- Pagination: See `PAGINATION_DROPDOWN_GUIDE.md`
