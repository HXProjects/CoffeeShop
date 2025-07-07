# Test Case: shop-web-004

## Title
Web Application - Session Persistence

## Domain
Web

## Description
Verify that basket contents persist across browser sessions and page refreshes.

## Preconditions
- Web application is running
- Browser supports session storage

## Test Steps
1. Add items to basket from home page
2. Navigate away from site or refresh page
3. Return to home page
4. Navigate to basket page
5. Verify basket contents are preserved
6. Verify basket counter (if displayed) shows correct count

## Expected Result
- Basket contents persist across page refreshes
- Session-based storage maintains state
- Basket counter displays correct item count
- All previously added items remain with correct quantities

## Test Data
- Add multiple different products to basket
- Test various navigation patterns
- Verify persistence across multiple page loads