using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.Json;

namespace CoffeeShop.UITests;

[TestFixture]
public class ApiProductTests
{
    private const string BaseApiUrl = "https://localhost:7073";
    private IAPIRequestContext? ApiContext;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        ApiContext = await playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = BaseApiUrl,
            IgnoreHTTPSErrors = true,
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                ["User-Agent"] = "Playwright-Test"
            }
        });
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        if (ApiContext != null)
            await ApiContext.DisposeAsync();
    }


    [Test]
    public async Task ShopProducts001_GetAllProducts()
    {
        var response = await ApiContext!.GetAsync("/api/products");
        var productsJson = await response.TextAsync();
        
        if (response.Status == 404)
        {
            Assert.Inconclusive($"API endpoint /products not found. Make sure API server is running on {BaseApiUrl}");
        }
        Assert.That(response.Status, Is.EqualTo(200), $"API returned status {response.Status}. Response: {productsJson}");
        Assert.That(string.IsNullOrEmpty(productsJson), Is.False, "API returned empty response");
        
        using var doc = JsonDocument.Parse(productsJson);
        Assert.That(doc.RootElement.GetArrayLength(), Is.GreaterThan(0));
        
        var firstProduct = doc.RootElement[0];
        Assert.That(firstProduct.TryGetProperty("id", out _), Is.True);
        Assert.That(firstProduct.TryGetProperty("name", out _), Is.True);
        Assert.That(firstProduct.TryGetProperty("price", out _), Is.True);
        Assert.That(firstProduct.TryGetProperty("category", out _), Is.True);
    }

    [Test]
    public async Task ShopProducts002_FilterProductsByCategory()
    {
        var response = await ApiContext!.GetAsync("/api/products?category=Coffee");
        var productsJson = await response.TextAsync();
        using var doc = JsonDocument.Parse(productsJson);

        Assert.That(response.Status, Is.EqualTo(200));
        
        foreach (var product in doc.RootElement.EnumerateArray())
        {
            Assert.That(product.GetProperty("category").GetString(), Is.EqualTo("Coffee"));
        }
    }

    [Test]
    public async Task ShopProducts003_SearchProductsByName()
    {
        var response = await ApiContext!.GetAsync("/api/products?search=Latte");
        var productsJson = await response.TextAsync();
        using var doc = JsonDocument.Parse(productsJson);

        Assert.That(response.Status, Is.EqualTo(200));
        
        foreach (var product in doc.RootElement.EnumerateArray())
        {
            var productName = product.GetProperty("name").GetString()!;
            Assert.That(productName.Contains("Latte", StringComparison.OrdinalIgnoreCase), Is.True);
        }
    }

    [Test]
    public async Task ShopProducts004_GetProductByValidId()
    {
        var response = await ApiContext!.GetAsync("/api/products/1");
        var productJson = await response.TextAsync();
        using var doc = JsonDocument.Parse(productJson);

        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(doc.RootElement.GetProperty("id").GetInt32(), Is.EqualTo(1));
        Assert.That(doc.RootElement.TryGetProperty("name", out _), Is.True);
        Assert.That(doc.RootElement.TryGetProperty("price", out _), Is.True);
        Assert.That(doc.RootElement.TryGetProperty("category", out _), Is.True);
    }

    [Test]
    public async Task ShopProducts005_GetProductByInvalidId()
    {
        var response = await ApiContext!.GetAsync("/api/products/9999");

        Assert.That(response.Status, Is.EqualTo(404));
    }

    [Test]
    public async Task ShopCategories001_GetAllCategories()
    {
        var response = await ApiContext!.GetAsync("/api/products/categories");
        var categoriesJson = await response.TextAsync();
        using var doc = JsonDocument.Parse(categoriesJson);

        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(doc.RootElement.GetArrayLength(), Is.GreaterThan(0));
        
        var categories = doc.RootElement.EnumerateArray().Select(c => c.GetString()).ToList();
        var expectedCategories = new[] { "Coffee", "Cold Coffee", "Food", "Pastry" };
        
        foreach (var expectedCategory in expectedCategories)
        {
            Assert.That(categories.Contains(expectedCategory), Is.True, $"Expected category '{expectedCategory}' not found");
        }
    }
}
