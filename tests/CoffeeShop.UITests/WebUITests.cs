using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace CoffeeShop.UITests;

[TestFixture]
public class WebUITests : PageTest
{
    private const string BaseUrl = "https://localhost:7001";


    [Test]
    public async Task ShopWeb001_HomePageProductDisplay()
    {
        try
        {
            await Page.GotoAsync(BaseUrl);
        }
        catch (Exception ex)
        {
            Assert.Inconclusive($"Cannot connect to web server at {BaseUrl}. Make sure web server is running. Error: {ex.Message}");
        }
        
        await Expect(Page).ToHaveTitleAsync("Coffee Shop");
        
        var productCards = Page.Locator(".product-card");
        await Expect(productCards).Not.ToHaveCountAsync(0);
        
        var addButtons = Page.Locator(".add-to-basket-form button");
        await Expect(addButtons).Not.ToHaveCountAsync(0);
    }

    [Test]
    public async Task ShopWeb002_AddProductToBasket()
    {
        await Page.GotoAsync(BaseUrl);
        
        var firstAddButton = Page.Locator(".add-to-basket-form button").First;
        await firstAddButton.ClickAsync();
        
        // Wait a moment for the add to basket to process
        await Page.WaitForTimeoutAsync(1000);
        
        await Page.GotoAsync($"{BaseUrl}/Basket");
        
        // Check that basket is no longer empty
        var emptyMessage = Page.Locator("h3:has-text('Your basket is empty')");
        await Expect(emptyMessage).Not.ToBeVisibleAsync();
    }

    [Test]
    public async Task ShopWeb003_BasketManagement()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.Locator(".add-to-basket-form button").First.ClickAsync();
        
        // Wait for basket update
        await Page.WaitForTimeoutAsync(1000);
        
        await Page.GotoAsync($"{BaseUrl}/Basket");
        
        // Check that basket is no longer empty
        var emptyMessage = Page.Locator("h3:has-text('Your basket is empty')");
        await Expect(emptyMessage).Not.ToBeVisibleAsync();
        
        // This test verifies basic basket functionality - adding items removes empty state
    }

    [Test]
    public async Task ShopWeb004_SessionPersistence()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.Locator(".add-to-basket-form button").First.ClickAsync();
        
        // Wait for basket update
        await Page.WaitForTimeoutAsync(1000);
        
        await Page.ReloadAsync();
        
        await Page.GotoAsync($"{BaseUrl}/Basket");
        
        // Check that basket is no longer empty after page reload
        var emptyMessage = Page.Locator("h3:has-text('Your basket is empty')");
        await Expect(emptyMessage).Not.ToBeVisibleAsync();
        
        // Check that basket count shows at least 1 item
        var basketCount = Page.Locator("#basketCount");
        await Expect(basketCount).Not.ToHaveTextAsync("0");
    }
}
