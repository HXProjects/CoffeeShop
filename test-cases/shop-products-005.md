# Test Case: shop-products-005

## Title
Get Product by Invalid ID

## Domain
Products

## Description
Verify that the system returns 404 Not Found when an invalid product ID is provided.

## Preconditions
- Database contains seeded products
- Product with ID = 9999 does not exist

## Test Steps
1. Send GET request to `/api/products/9999`
2. Verify response status is 404 Not Found

## Expected Result
- Response status: 404 Not Found
- Response body: Empty or error message

## Test Data
- Product ID: 9999 (non-existent ID)