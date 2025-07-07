# Test Case: shop-basket-009

## Title
Add Item with Invalid Product ID

## Domain
Basket

## Description
Verify that adding an item with non-existent product ID returns appropriate error.

## Preconditions
- Database contains seeded products
- Product with ID = 9999 does not exist

## Test Steps
1. Send POST request to `/api/basket/{sessionId}/items` with body:
   ```json
   {
     "productId": 9999,
     "quantity": 1
   }
   ```
2. Verify response status is 400 Bad Request or 404 Not Found
3. Verify appropriate error message is returned

## Expected Result
- Response status: 400 Bad Request or 404 Not Found
- Response contains error message indicating invalid product ID
- No item added to basket

## Test Data
- Session ID: Valid session
- Product ID: 9999 (non-existent)
- Quantity: 1