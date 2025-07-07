# Test Case: shop-basket-008

## Title
Remove Item by Setting Quantity to Zero

## Domain
Basket

## Description
Verify that setting an item's quantity to zero removes it from the basket.

## Preconditions
- Database contains seeded products
- Basket contains item with ID = 1, ProductId = 1, Quantity = 2

## Test Steps
1. Send PUT request to `/api/basket/{sessionId}/items/1` with body:
   ```json
   {
     "quantity": 0
   }
   ```
2. Verify response status is 200 OK
3. Send GET request to `/api/basket/{sessionId}` to verify item removed
4. Verify basket no longer contains the item

## Expected Result
- PUT response status: 200 OK
- GET response shows basket without the item
- Item automatically removed when quantity set to 0

## Test Data
- Session ID: Existing session
- Basket Item ID: 1
- New quantity: 0