# Test Case: shop-basket-007

## Title
Get Basket Summary with Tax Calculation

## Domain
Basket

## Description
Verify that the basket summary correctly calculates subtotal, tax (8%), and total.

## Preconditions
- Database contains seeded products with known prices
- Basket contains items with known quantities and unit prices

## Test Steps
1. Add items to basket with known prices (e.g., Product1=$5.00 Qty=2, Product2=$3.00 Qty=1)
2. Send GET request to `/api/basket/{sessionId}/summary`
3. Verify response status is 200 OK
4. Verify calculations are correct

## Expected Result
- Response status: 200 OK
- Response contains BasketSummary object
- TotalItems = 3 (2 + 1)
- Subtotal = $13.00 (5.00*2 + 3.00*1)
- Tax = $1.04 (13.00 * 0.08)
- Total = $14.04 (13.00 + 1.04)

## Test Data
- Product1: Price=$5.00, Quantity=2
- Product2: Price=$3.00, Quantity=1
- Tax rate: 8%