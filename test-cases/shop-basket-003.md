# Test Case: shop-basket-003

## Title
Update Basket Item Quantity

## Domain
Basket

## Description
Verify that the quantity of an existing basket item can be updated.

## Preconditions
- Database contains seeded products
- Basket contains item with ID = 1, ProductId = 1, Quantity = 2

## Test Steps
1. Send PUT request to `/api/basket/{sessionId}/items/1` with body:
   ```json
   {
     "quantity": 5
   }
   ```
2. Verify response status is 200 OK
3. Send GET request to `/api/basket/{sessionId}` to verify quantity updated
4. Verify item quantity changed to 5

## Expected Result
- PUT response status: 200 OK
- GET response shows updated quantity
- Item has same ProductId but Quantity = 5
- UnitPrice remains unchanged

## Test Data
- Session ID: Existing session
- Basket Item ID: 1
- New quantity: 5