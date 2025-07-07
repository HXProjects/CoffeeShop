# Test Case: shop-security-001

## Title
Session Security and Isolation

## Domain
Security

## Description
Verify that session-based basket isolation prevents unauthorized access to other users' baskets.

## Preconditions
- API service is running
- Multiple session IDs available for testing

## Test Steps
1. Create basket items for SessionId-A
2. Create basket items for SessionId-B
3. Attempt to access SessionId-A basket using SessionId-B
4. Attempt to modify SessionId-A basket using SessionId-B
5. Verify no cross-session data access is possible

## Expected Result
- Each session can only access its own basket
- Attempts to access other sessions return empty or error
- No data leakage between sessions
- Session IDs provide adequate isolation
- Unauthorized access attempts are blocked

## Test Data
- SessionId-A: Valid GUID with basket items
- SessionId-B: Different valid GUID with different items
- Invalid session IDs for negative testing