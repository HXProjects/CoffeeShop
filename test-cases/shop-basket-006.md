# Test Case: shop-basket-006

## Title
Get Basket Item Count

## Domain
Basket

## Description
Verify that the system correctly returns the total count of items in the basket.

## Preconditions
- Database contains seeded products
- Basket contains 2 items: ProductId=1 (Qty=3), ProductId=2 (Qty=2)

## Test Steps
1. Send GET request to `/api/basket/{sessionId}/count`
2. Verify response status is 200 OK
3. Verify response returns total item count

## Expected Result
- Response status: 200 OK
- Response body: Integer value = 5 (3 + 2)
- Count represents total quantity across all items

## Test Data
- Session ID: Existing session
- Item 1: ProductId=1, Quantity=3
- Item 2: ProductId=2, Quantity=2
- Expected count: 5