# Test Case: shop-categories-001

## Title
Get All Product Categories

## Domain
Categories

## Description
Verify that the system returns all unique categories from available products.

## Preconditions
- Database contains seeded products with different categories
- Products exist in categories: Coffee, Cold Coffee, Pastry, Food

## Test Steps
1. Send GET request to `/api/products/categories`
2. Verify response status is 200 OK
3. Verify response contains array of category strings
4. Verify all expected categories are present
5. Verify categories are unique (no duplicates)

## Expected Result
- Response status: 200 OK
- Response body: Array of strings representing categories
- Categories include: "Coffee", "Cold Coffee", "Pastry", "Food"
- No duplicate categories
- Only categories from available products are included

## Test Data
Expected categories: ["Coffee", "Cold Coffee", "Food", "Pastry"] (alphabetically sorted)