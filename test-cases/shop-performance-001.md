# Test Case: shop-performance-001

## Title
API Response Performance

## Domain
Performance

## Description
Verify that API endpoints respond within acceptable time limits under normal load.

## Preconditions
- API service is running
- Database contains seeded products
- Normal system load conditions

## Test Steps
1. Measure response time for GET /api/products
2. Measure response time for GET /api/products/{id}
3. Measure response time for GET /api/products/categories
4. Measure response time for basket operations
5. Repeat measurements multiple times for consistency

## Expected Result
- GET /api/products: < 500ms
- GET /api/products/{id}: < 200ms
- GET /api/products/categories: < 300ms
- Basket operations: < 400ms
- Consistent performance across multiple requests

## Test Data
- Standard seeded database (12 products)
- Multiple API calls for statistical accuracy
- Normal system resource availability