# Test Case: shop-products-002

## Title
Get Products by Category Filter

## Domain
Products

## Description
Verify that the system correctly filters products by category when category parameter is provided.

## Preconditions
- Database contains products with different categories (Coffee, Cold Coffee, Pastry, Food)
- At least one product exists in "Coffee" category

## Test Steps
1. Send GET request to `/api/products?category=Coffee`
2. Verify response status is 200 OK
3. Verify response contains only products with Category = "Coffee"
4. Verify all returned products have IsAvailable = true
5. Verify products are ordered by Name

## Expected Result
- Response status: 200 OK
- Response body: Array of Product objects filtered by category
- All products have Category = "Coffee"
- All products have IsAvailable = true
- Products sorted by Name (alphabetically)

## Test Data
- Filter category: "Coffee"
- Expected products: Espresso, Americano, Cappuccino, Latte, Mocha, Macchiato, Flat White