# Test Case: shop-basket-010

## Title
Session Isolation - Different Sessions Have Separate Baskets

## Domain
Basket

## Description
Verify that different session IDs maintain separate basket contents.

## Preconditions
- Database contains seeded products
- Two different session IDs: Session1 and Session2

## Test Steps
1. Add Product1 to Session1 basket
2. Add Product2 to Session2 basket
3. Send GET request to `/api/basket/{Session1}`
4. Send GET request to `/api/basket/{Session2}`
5. Verify each session contains only its own items

## Expected Result
- Session1 basket contains only Product1
- Session2 basket contains only Product2
- No cross-contamination between sessions
- Each session maintains separate state

## Test Data
- Session1 ID: GUID-1
- Session2 ID: GUID-2
- Product1 ID: 1
- Product2 ID: 2