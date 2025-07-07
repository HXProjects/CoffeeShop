# Test Case: shop-basket-004

## Title
Remove Item from Basket

## Domain
Basket

## Description
Verify that an item can be removed from the basket.

## Preconditions
- Database contains seeded products
- Basket contains item with ID = 1, ProductId = 1, Quantity = 2

## Test Steps
1. Send DELETE request to `/api/basket/{sessionId}/items/1`
2. Verify response status is 200 OK or 204 No Content
3. Send GET request to `/api/basket/{sessionId}` to verify item removed
4. Verify basket no longer contains the item

## Expected Result
- DELETE response status: 200 OK or 204 No Content
- GET response shows basket without the removed item
- Basket is empty if only one item existed

## Test Data
- Session ID: Existing session
- Basket Item ID: 1