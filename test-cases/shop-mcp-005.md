# Test Case: shop-mcp-005

## Title
MCP GetCategororiesTool - Retrieve Product Categories

## Domain
MCP (Model Context Protocol)

## Description
Verify that the MCP GetCategororiesTool correctly retrieves all available product categories.

## Preconditions
- MCP Server is running and accessible
- Database contains seeded products with categories
- GetCategororiesTool is registered and available

## Test Steps
1. Call GetCategororiesTool with no parameters
2. Verify successful response
3. Verify response contains array of category strings
4. Verify all expected categories are present
5. Verify no duplicates exist

## Expected Result
- Tool execution successful
- Response contains array of unique category strings
- Categories include: "Coffee", "Cold Coffee", "Pastry", "Food"
- Categories are derived from available products only
- No duplicate categories

## Test Data
Expected categories: ["Coffee", "Cold Coffee", "Food", "Pastry"]