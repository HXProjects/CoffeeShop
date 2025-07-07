# Test Case: shop-mcp-003

## Title
MCP AddToBasketTool - Add Product to Basket

## Domain
MCP (Model Context Protocol)

## Description
Verify that the MCP AddToBasketTool correctly adds a product to the basket.

## Preconditions
- MCP Server is running and accessible
- Database contains seeded products
- Product with ID = 1 exists and is available
- AddToBasketTool is registered and available

## Test Steps
1. Call AddToBasketTool with parameters:
   - sessionId: valid GUID
   - productId: 1
   - quantity: 2
2. Verify successful response
3. Use GetBasketTool to verify item was added
4. Verify basket contains the added item with correct details

## Expected Result
- Tool execution successful
- Item added to basket successfully
- Basket contains ProductId = 1, Quantity = 2
- UnitPrice matches product's current price

## Test Data
- Session ID: Valid GUID
- Product ID: 1
- Quantity: 2