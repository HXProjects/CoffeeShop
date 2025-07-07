# Test Case: shop-integration-001

## Title
End-to-End Integration - Complete Shopping Flow

## Domain
Integration

## Description
Verify complete shopping flow from product browsing to basket management across API and Web interfaces.

## Preconditions
- All services running (API, Web, MCP Server)
- Database contains seeded products
- Clean test environment

## Test Steps
1. **API Testing**: Get products via API endpoint
2. **Web Testing**: Browse products on web interface
3. **MCP Testing**: Use MCP tools to retrieve products
4. **Cross-Interface**: Add items via Web, verify via API
5. **Session Consistency**: Verify same session works across interfaces
6. **Data Consistency**: Verify all interfaces show same data

## Expected Result
- All interfaces return consistent product data
- Session state is maintained across API and Web
- MCP tools interact correctly with same database
- No data discrepancies between interfaces
- Complete shopping flow works end-to-end

## Test Data
- Use multiple products and categories
- Test with various session scenarios
- Verify data consistency across all touch points