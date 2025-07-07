using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.Json;

namespace CoffeeShop.UITests;

[TestFixture]
public class ApiBasketTests
{
    private const string BaseApiUrl = "https://localhost:7073";
    private IAPIRequestContext? ApiContext;
    private readonly string SessionId = Guid.NewGuid().ToString();

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
    public async Task ShopBasket001_AddItemToEmptyBasket()
    {
        var response = await ApiContext!.PostAsync($"/api/basket/{SessionId}/items", new()
        {
            DataObject = new { productId = 1, quantity = 2 }
        });

        Assert.That(response.Status, Is.EqualTo(200).Or.EqualTo(201), $"Add item failed with status {response.Status}. Response: {await response.TextAsync()}");
        
        var basketResponse = await ApiContext.GetAsync($"/api/basket/{SessionId}");
        var basketJson = await basketResponse.TextAsync();
        Assert.That(string.IsNullOrEmpty(basketJson), Is.False, "Basket API returned empty response");
        using var doc = JsonDocument.Parse(basketJson);
        
        Assert.That(doc.RootElement.GetArrayLength(), Is.EqualTo(1));
        Assert.That(doc.RootElement[0].GetProperty("quantity").GetInt32(), Is.EqualTo(2));
    }




    [Test]
    public async Task ShopBasket005_ClearEntireBasket()
    {
        await ApiContext!.PostAsync($"/api/basket/{SessionId}/items", new()
        {
            DataObject = new { productId = 1, quantity = 2 }
        });
        await ApiContext.PostAsync($"/api/basket/{SessionId}/items", new()
        {
            DataObject = new { productId = 2, quantity = 1 }
        });

        var response = await ApiContext.DeleteAsync($"/api/basket/{SessionId}");

        Assert.That(response.Status, Is.EqualTo(200).Or.EqualTo(204));
        
        var basketResponse = await ApiContext.GetAsync($"/api/basket/{SessionId}");
        var basketJson = await basketResponse.TextAsync();
        using var doc = JsonDocument.Parse(basketJson);
        
        Assert.That(doc.RootElement.GetArrayLength(), Is.EqualTo(0));
    }

    [Test]
    public async Task ShopBasket006_GetBasketItemCount()
    {
        await ApiContext!.PostAsync($"/api/basket/{SessionId}/items", new()
        {
            DataObject = new { productId = 1, quantity = 3 }
        });
        await ApiContext.PostAsync($"/api/basket/{SessionId}/items", new()
        {
            DataObject = new { productId = 2, quantity = 2 }
        });

        var response = await ApiContext.GetAsync($"/api/basket/{SessionId}/count");
        var countText = await response.TextAsync();
        using var doc = JsonDocument.Parse(countText);

        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(doc.RootElement.GetProperty("count").GetInt32(), Is.EqualTo(5));
    }

    [Test]
    public async Task ShopBasket007_BasketSummaryWithTax()
    {
        await ApiContext!.PostAsync($"/api/basket/{SessionId}/items", new()
        {
            DataObject = new { productId = 1, quantity = 2 }
        });

        var response = await ApiContext.GetAsync($"/api/basket/{SessionId}/summary");
        var summaryJson = await response.TextAsync();
        using var doc = JsonDocument.Parse(summaryJson);

        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(doc.RootElement.GetProperty("subtotal").GetDecimal(), Is.GreaterThan(0));
        Assert.That(doc.RootElement.GetProperty("tax").GetDecimal(), Is.GreaterThan(0));
        Assert.That(doc.RootElement.GetProperty("total").GetDecimal(), Is.GreaterThan(doc.RootElement.GetProperty("subtotal").GetDecimal()));
        
        var expectedTax = doc.RootElement.GetProperty("subtotal").GetDecimal() * 0.08m;
        Assert.That(doc.RootElement.GetProperty("tax").GetDecimal(), Is.EqualTo(expectedTax).Within(0.01m));
    }


    [Test]
    public async Task ShopBasket009_InvalidProductIdError()
    {
        var response = await ApiContext!.PostAsync($"/api/basket/{SessionId}/items", new()
        {
            DataObject = new { productId = 9999, quantity = 1 }
        });

        Assert.That(response.Status, Is.EqualTo(400).Or.EqualTo(404));
    }

    [Test]
    public async Task ShopBasket010_SessionIsolation()
    {
        var session1 = Guid.NewGuid().ToString();
        var session2 = Guid.NewGuid().ToString();

        await ApiContext!.PostAsync($"/api/basket/{session1}/items", new()
        {
            DataObject = new { productId = 1, quantity = 2 }
        });
        await ApiContext.PostAsync($"/api/basket/{session2}/items", new()
        {
            DataObject = new { productId = 2, quantity = 3 }
        });

        var basket1Response = await ApiContext.GetAsync($"/api/basket/{session1}");
        var basket1Json = await basket1Response.TextAsync();
        using var doc1 = JsonDocument.Parse(basket1Json);
        
        var basket2Response = await ApiContext.GetAsync($"/api/basket/{session2}");
        var basket2Json = await basket2Response.TextAsync();
        using var doc2 = JsonDocument.Parse(basket2Json);

        Assert.That(doc1.RootElement.GetArrayLength(), Is.EqualTo(1));
        Assert.That(doc2.RootElement.GetArrayLength(), Is.EqualTo(1));
        Assert.That(doc1.RootElement[0].GetProperty("productId").GetInt32(), Is.Not.EqualTo(doc2.RootElement[0].GetProperty("productId").GetInt32()));
    }
}
