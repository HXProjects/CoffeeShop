# Test Case: shop-mcp-002

## Title
MCP GetProductTool - Retrieve Specific Product

## Domain
MCP (Model Context Protocol)

## Description
Verify that the MCP GetProductTool correctly retrieves a specific product by ID.

## Preconditions
- MCP Server is running and accessible
- Database contains seeded products
- Product with ID = 1 exists
- GetProductTool is registered and available

## Test Steps
1. Call GetProductTool with parameter: productId = 1
2. Verify successful response
3. Verify response contains single Product object
4. Verify product has correct ID and all required fields

## Expected Result
- Tool execution successful
- Response contains single Product object
- Product.Id = 1
- All required fields present and valid
- Product data matches database record

## Test Data
- Product ID: 1
- Expected product: First seeded product (typically Espresso)