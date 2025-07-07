# Test Case: shop-web-003

## Title
Web Application - Basket Management

## Domain
Web

## Description
Verify that the basket page allows updating quantities and removing items.

## Preconditions
- Web application is running
- Basket contains at least one item

## Test Steps
1. Navigate to basket page (/Basket)
2. Verify basket items are displayed correctly
3. Update quantity of an item using quantity controls
4. Verify total updates automatically
5. Remove an item from basket
6. Verify item is removed and totals update
7. Verify tax calculation (8%) is correct

## Expected Result
- Basket page displays all items correctly
- Quantity updates work and recalculate totals
- Item removal works correctly
- Subtotal, tax (8%), and total calculations are accurate
- Empty basket message appears when all items removed

## Test Data
- Initial basket with multiple items
- Test quantity changes: increase and decrease
- Verify tax calculation at 8% rate