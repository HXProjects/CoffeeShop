# Test Case: shop-products-001

## Title
Get All Available Products - Happy Path

## Domain
Products

## Description
Verify that the system returns all available products when no filters are applied.

## Preconditions
- Database contains seeded products
- At least one product has IsAvailable = true

## Test Steps
1. Send GET request to `/api/products`
2. Verify response status is 200 OK
3. Verify response contains array of products
4. Verify all returned products have IsAvailable = true
5. Verify products are ordered by Category, then Name

## Expected Result
- Response status: 200 OK
- Response body: Array of Product objects
- All products have IsAvailable = true
- Products sorted by Category (alphabetically), then Name (alphabetically)
- Response includes all required fields: Id, Name, Description, Price, ImageUrl, Category, IsAvailable

## Test Data
Use seeded database with standard coffee shop products (Coffee, Cold Coffee, Pastry, Food categories)