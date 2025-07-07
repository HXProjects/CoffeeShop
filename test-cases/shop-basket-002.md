# Test Case: shop-basket-002

## Title
Add Duplicate Product to Basket

## Domain
Basket

## Description
Verify that adding the same product to basket merges quantities instead of creating duplicate items.

## Preconditions
- Database contains seeded products
- Product with ID = 1 exists and is available
- Basket already contains 1 item of Product ID = 1 with quantity = 2

## Test Steps
1. Send POST request to `/api/basket/{sessionId}/items` with body:
   ```json
   {
     "productId": 1,
     "quantity": 3
   }
   ```
2. Verify response status is 200 OK
3. Send GET request to `/api/basket/{sessionId}` to verify quantities merged
4. Verify basket contains only one item for Product ID = 1 with combined quantity

## Expected Result
- POST response status: 200 OK
- GET response shows basket with one item
- Item has ProductId = 1, Quantity = 5 (2 + 3)
- No duplicate items in basket

## Test Data
- Session ID: Existing session with item
- Product ID: 1 (same as existing)
- New quantity: 3
- Expected total quantity: 5