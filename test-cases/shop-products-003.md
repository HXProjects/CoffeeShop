# Test Case: shop-products-003

## Title
Search Products by Name

## Domain
Products

## Description
Verify that the system correctly searches products by name when search parameter is provided.

## Preconditions
- Database contains seeded products
- Product "Latte" exists in database

## Test Steps
1. Send GET request to `/api/products?search=Latte`
2. Verify response status is 200 OK
3. Verify response contains products where Name contains "Latte"
4. Verify all returned products have IsAvailable = true

## Expected Result
- Response status: 200 OK
- Response body: Array of Product objects matching search criteria
- Products include "Latte" and "Iced Latte"
- All products have IsAvailable = true

## Test Data
- Search term: "Latte"
- Expected matches: "Latte", "Iced Latte"