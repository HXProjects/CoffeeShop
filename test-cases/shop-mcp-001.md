# Test Case: shop-mcp-001

## Title
MCP GetProductsTool - Retrieve All Products

## Domain
MCP (Model Context Protocol)

## Description
Verify that the MCP GetProductsTool correctly retrieves all available products.

## Preconditions
- MCP Server is running and accessible
- Database contains seeded products
- GetProductsTool is registered and available

## Test Steps
1. Call GetProductsTool with no parameters
2. Verify successful response
3. Verify response contains array of products
4. Verify all products have IsAvailable = true
5. Verify product data structure is complete

## Expected Result
- Tool execution successful
- Response contains array of Product objects
- All products have IsAvailable = true
- Products include all required fields: Id, Name, Description, Price, ImageUrl, Category, IsAvailable
- Products are ordered appropriately

## Test Data
Expected products from seeded database (12 products across 4 categories)