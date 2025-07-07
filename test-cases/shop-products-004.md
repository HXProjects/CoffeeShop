# Test Case: shop-products-004

## Title
Get Product by Valid ID

## Domain
Products

## Description
Verify that the system returns a specific product when a valid product ID is provided.

## Preconditions
- Database contains seeded products
- Product with ID = 1 exists and is available

## Test Steps
1. Send GET request to `/api/products/1`
2. Verify response status is 200 OK
3. Verify response contains single Product object
4. Verify returned product has Id = 1
5. Verify all required fields are present and valid

## Expected Result
- Response status: 200 OK
- Response body: Single Product object
- Product.Id = 1
- All required fields present: Name, Description, Price, ImageUrl, Category, IsAvailable

## Test Data
- Product ID: 1
- Expected product: First seeded product (typically Espresso)