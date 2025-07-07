# Test Case: shop-web-002

## Title
Web Application - Add Product to Basket

## Domain
Web

## Description
Verify that products can be added to the basket from the web interface.

## Preconditions
- Web application is running
- Home page displays products
- Basket is initially empty

## Test Steps
1. Navigate to home page
2. Click "Add to Basket" button for a specific product
3. Verify success message or indication
4. Navigate to basket page (/Basket)
5. Verify product appears in basket
6. Verify quantity, price, and total are correct

## Expected Result
- Add to Basket action succeeds
- Product appears in basket with quantity = 1
- Correct product name, price displayed
- Basket total calculated correctly
- Session-based basket persistence works

## Test Data
- Selected product: Any available product from catalog
- Expected quantity: 1
- Expected price: Product's listed price