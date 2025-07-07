# Test Case: shop-mcp-004

## Title
MCP GetBasketTool - Retrieve Basket Contents

## Domain
MCP (Model Context Protocol)

## Description
Verify that the MCP GetBasketTool correctly retrieves basket contents and summary.

## Preconditions
- MCP Server is running and accessible
- Basket contains items for a specific session
- GetBasketTool is registered and available

## Test Steps
1. Add items to basket using AddToBasketTool
2. Call GetBasketTool with parameter: sessionId
3. Verify successful response
4. Verify response contains basket items and summary
5. Verify calculations are correct (subtotal, tax, total)

## Expected Result
- Tool execution successful
- Response contains basket items array
- Response contains basket summary with correct calculations
- Tax calculated at 8% rate
- Total = Subtotal + Tax

## Test Data
- Session ID: Valid GUID with existing basket items
- Expected items with quantities and prices
- Expected summary calculations