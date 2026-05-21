# Pagination in Dropdowns - Best Practices Guide

## Overview
Pagination is typically used for lists and tables, but can be adapted for dropdowns with large datasets. Here are the recommended patterns based on your StateController example.

---

## Pattern 1: Server-Side Pagination for Dropdowns

### When to Use
- Large datasets (1000+ items)
- API bandwidth concerns
- Real-time data that changes frequently

### Implementation

**Frontend (Dropdown with Pagination):**
```javascript
// Load initial page
function loadDropdownOptions(pageNumber = 1, pageSize = 50) {
  fetch(`/api/v1/state?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
    headers: { 'Authorization': `Bearer ${token}` }
  })
  .then(response => response.json())
  .then(data => {
    populateDropdown(data.data);
    updatePaginationButtons(data.pagination);
  });
}

// Populate dropdown with current page data
function populateDropdown(states) {
  const select = document.getElementById('stateDropdown');
  select.innerHTML = '';
  
  states.forEach(state => {
    const option = document.createElement('option');
    option.value = state.id;
    option.textContent = state.name;
    select.appendChild(option);
  });
}

// Update pagination controls
function updatePaginationButtons(pagination) {
  document.getElementById('pageInfo').textContent = 
    `Page ${pagination.pageNumber} of ${pagination.totalPages}`;
  
  document.getElementById('prevBtn').disabled = pagination.pageNumber === 1;
  document.getElementById('nextBtn').disabled = 
    pagination.pageNumber >= pagination.totalPages;
}

// Navigation functions
document.getElementById('prevBtn').onclick = () => 
  loadDropdownOptions(currentPage - 1);
  
document.getElementById('nextBtn').onclick = () => 
  loadDropdownOptions(currentPage + 1);
```

**HTML:**
```html
<div class="dropdown-pagination-container">
  <select id="stateDropdown" class="form-control">
    <option value="">Select State</option>
  </select>
  
  <div class="pagination-controls">
    <button id="prevBtn" class="btn btn-sm btn-secondary">Previous</button>
    <span id="pageInfo"></span>
    <button id="nextBtn" class="btn btn-sm btn-secondary">Next</button>
  </div>
</div>
```

---

## Pattern 2: Search + Pagination for Dropdowns

### When to Use
- Large dropdown lists
- Users need to search within options
- Filter results with pagination

### Implementation

**Frontend:**
```javascript
let currentSearchTerm = '';
let currentPage = 1;

function searchDropdown(searchTerm) {
  currentSearchTerm = searchTerm;
  currentPage = 1; // Reset to first page on new search
  
  const pageSize = 25;
  const searchQuery = searchTerm ? `&searchTerm=${searchTerm}` : '';
  
  fetch(`/api/v1/client/search?pageNumber=${currentPage}&pageSize=${pageSize}${searchQuery}`, {
    headers: { 'Authorization': `Bearer ${token}` }
  })
  .then(response => response.json())
  .then(data => {
    populateDropdown(data.data);
    updateSearchPagination(data.pagination);
  });
}

function updateSearchPagination(pagination) {
  const showPagination = pagination.totalPages > 1;
  document.getElementById('paginationContainer').style.display = 
    showPagination ? 'block' : 'none';
}
```

**HTML:**
```html
<div class="dropdown-search-pagination">
  <input type="text" id="searchInput" placeholder="Search clients..." 
         class="form-control mb-2"
         oninput="searchDropdown(this.value)">
  
  <select id="clientDropdown" class="form-control"></select>
  
  <div id="paginationContainer" class="pagination-controls mt-2" style="display:none;">
    <button onclick="previousPage()" class="btn btn-sm btn-outline-secondary">
      ← Previous
    </button>
    <span id="pageInfo" class="mx-2"></span>
    <button onclick="nextPage()" class="btn btn-sm btn-outline-secondary">
      Next →
    </button>
  </div>
</div>
```

---

## Pattern 3: Infinite Scroll (Virtual Scrolling)

### When to Use
- Very large datasets (10000+ items)
- Better UX than traditional pagination
- Mobile-friendly

### Implementation

```javascript
class VirtualDropdown {
  constructor(selectElement, apiUrl) {
    this.select = selectElement;
    this.apiUrl = apiUrl;
    this.pageSize = 30;
    this.currentPage = 1;
    this.items = [];
    this.isLoading = false;
    
    this.setupInfiniteScroll();
  }
  
  setupInfiniteScroll() {
    this.select.addEventListener('scroll', (e) => {
      const element = e.target;
      const scrollPosition = element.scrollTop + element.clientHeight;
      const threshold = element.scrollHeight - 50;
      
      if (scrollPosition >= threshold && !this.isLoading) {
        this.loadMore();
      }
    });
  }
  
  async loadMore() {
    this.isLoading = true;
    
    try {
      const response = await fetch(
        `${this.apiUrl}?pageNumber=${this.currentPage}&pageSize=${this.pageSize}`
      );
      const data = await response.json();
      
      this.items = [...this.items, ...data.data];
      this.renderOptions();
      
      this.currentPage++;
    } finally {
      this.isLoading = false;
    }
  }
  
  renderOptions() {
    this.select.innerHTML = '';
    
    this.items.forEach(item => {
      const option = document.createElement('option');
      option.value = item.id;
      option.textContent = item.name;
      this.select.appendChild(option);
    });
  }
}

// Usage
const dropdown = new VirtualDropdown(
  document.getElementById('stateDropdown'),
  '/api/v1/state'
);
```

---

## StateController Pagination Example

Your `StateController` uses pagination like this:

```csharp
// API Call
public async Task<IActionResult> GetAllAsync([FromQuery] GetStateMasterQuery query)
{
    PaginatedResult<GetStateMasterResponse> result = 
        await Mediator.Send(query, RequestAborted);
    return FromPaginated(result);
}
```

**Query Class:**
```csharp
public class GetStateMasterQuery : IRequest<PaginatedResult<GetStateMasterResponse>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
```

**To Use in Dropdown Frontend:**
```javascript
// Load states with pagination
async function loadStates(pageNumber = 1) {
  const response = await fetch(
    `/api/v1/state?pageNumber=${pageNumber}&pageSize=50`,
    { headers: { 'Authorization': `Bearer ${token}` } }
  );
  
  const apiResponse = response.json();
  const { data, pagination } = apiResponse;
  
  // data: Array of states
  // pagination: { pageNumber, pageSize, totalPages, totalCount }
  
  populateStateDropdown(data);
  updatePaginationUI(pagination);
}
```

---

## Recommended Approach for Your App

Based on your architecture, here's what I recommend:

### For States (Smaller Dataset)
```
✓ Load all at once (client-side pagination optional)
✓ Cache in memory
✓ Use simple select/dropdown
```

### For Clients (Medium Dataset)
```
✓ Server-side pagination (25-50 per page)
✓ Add search functionality
✓ Show pagination controls below dropdown
```

### For Cases (Large Dataset)
```
✓ Search + Server-side pagination
✓ Implement infinite scroll if 1000+ items
✓ Use virtual scrolling for performance
```

---

## Response Format

Your API returns paginated results like:

```json
{
  "succeeded": true,
  "data": [
    { "id": "uuid", "name": "State Name", ... },
    ...
  ],
  "pagination": {
    "pageNumber": 1,
    "pageSize": 50,
    "totalCount": 350,
    "totalPages": 7
  },
  "message": "Success",
  "statusCode": 200
}
```

---

## Best Practices

1. **Cache Frontend Data**
   ```javascript
   const cache = {};
   
   async function getStatesPagedAndCached(page) {
     const key = `states_page_${page}`;
     if (cache[key]) return cache[key];
     
     const data = await fetch(`/api/v1/state?pageNumber=${page}`);
     cache[key] = data;
     return data;
   }
   ```

2. **Debounce Search**
   ```javascript
   const debounce = (fn, delay) => {
     let timeout;
     return (...args) => {
       clearTimeout(timeout);
       timeout = setTimeout(() => fn(...args), delay);
     };
   };
   
   const searchDropdown = debounce((term) => {
     fetchDropdownOptions(term);
   }, 300);
   ```

3. **Show Loading States**
   ```javascript
   async function loadOptions(page) {
     dropdown.innerHTML = '<option>Loading...</option>';
     const data = await fetch(`...?pageNumber=${page}`);
     populateDropdown(data);
   }
   ```

4. **Handle Empty Results**
   ```javascript
   if (data.pagination.totalCount === 0) {
     dropdown.innerHTML = '<option>No results found</option>';
   }
   ```

---

## Summary Table

| Scenario | Pattern | Page Size | Notes |
|----------|---------|-----------|-------|
| States (< 100 items) | No pagination | All | Cache locally |
| Clients (100-1000) | Server pagination | 25-50 | Add search |
| Cases (1000+) | Infinite scroll | 30 | Use virtual scrolling |
| Dropdowns (Mobile) | Search + Pagination | 15-20 | Smaller pages |

---

## Postman Testing

For pagination in Postman:

```
GET /api/v1/state?pageNumber=1&pageSize=50
GET /api/v1/client/search?searchTerm=john&pageNumber=1&pageSize=25
```

**Response includes:**
```
data: [...]           // Current page items
pagination: {
  pageNumber: 1
  pageSize: 50
  totalCount: 350
  totalPages: 7
}
```
