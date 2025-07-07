# Test Case: shop-basket-005

## Title
Clear Entire Basket

## Domain
Basket

## Description
Verify that all items can be removed from the basket at once.

## Preconditions
- Database contains seeded products
- Basket contains multiple items (at least 2 different products)

## Test Steps
1. Send DELETE request to `/api/basket/{sessionId}`
2. Verify response status is 200 OK or 204 No Content
3. Send GET request to `/api/basket/{sessionId}` to verify basket is empty
4. Verify no items remain in basket

## Expected Result
- DELETE response status: 200 OK or 204 No Content
- GET response shows empty basket (empty array)
- All items removed from basket

## Test Data
- Session ID: Existing session with multiple items