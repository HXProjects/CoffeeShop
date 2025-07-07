# Test Case: shop-basket-001

## Title
Add Item to Empty Basket

## Domain
Basket

## Description
Verify that a product can be successfully added to an empty basket.

## Preconditions
- Database contains seeded products
- Product with ID = 1 exists and is available
- Basket for session is empty

## Test Steps
1. Send POST request to `/api/basket/{sessionId}/items` with body:
   ```json
   {
     "productId": 1,
     "quantity": 2
   }
   ```
2. Verify response status is 200 OK or 201 Created
3. Send GET request to `/api/basket/{sessionId}` to verify item was added
4. Verify basket contains the added item with correct quantity

## Expected Result
- POST response status: 200 OK or 201 Created
- GET response shows basket with one item
- Item has ProductId = 1, Quantity = 2
- UnitPrice matches product's current price

## Test Data
- Session ID: New GUID
- Product ID: 1
- Quantity: 2