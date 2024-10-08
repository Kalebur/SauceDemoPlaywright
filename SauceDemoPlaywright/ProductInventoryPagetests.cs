﻿using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace SauceDemoPlaywright
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class ProductInventoryPageTests : PageTest
    {
        private IPage _page;
        private LoginPage _loginPage;
        private ProductInventoryPage _productInventoryPage;
        private SauceDemoHelpers _sauceDemoHelpers;

        [SetUp]
        public async Task Setup()
        {
            _page = await Browser.NewPageAsync();
            _loginPage = new LoginPage(_page);
            _productInventoryPage = new ProductInventoryPage(_page);
            _sauceDemoHelpers = new SauceDemoHelpers(_page, _loginPage, _productInventoryPage);
            await _sauceDemoHelpers.LoginAs("standard_user");
        }

        [Test]
        public async Task HasAtLeastOneInventoryItemOnPage()
        {
            await Expect(_productInventoryPage.InventoryItems).ToHaveCountAsync(6);
        }

        [Test]
        public async Task HasHamburgerMenu()
        {
            await Expect(_productInventoryPage.HamburgerMenuOpenButton).ToBeVisibleAsync();
        }

        [Test]
        public async Task CanLogoutFromProductPage()
        {
            await _sauceDemoHelpers.Logout();

            await Expect(_page).ToHaveURLAsync(_loginPage.Url);
        }

        [Test]
        public async Task BrowserHasCookies()
        {
            foreach (var context in Browser.Contexts)
            {
                var cookies = await context.CookiesAsync(new string[] { _loginPage.Url, });
                await Console.Out.WriteLineAsync("Context: " + context);
                foreach (var cookie in cookies)
                {
                    await Console.Out.WriteLineAsync(cookie.Name);
                    await Console.Out.WriteLineAsync(cookie.Value);
                    await Console.Out.WriteLineAsync(cookie.Expires.ToString());
                    cookie.Expires += 2000000000f;
                    await Console.Out.WriteLineAsync(cookie.Expires.ToString());
                }
            }
        }

        [Test]
        public async Task ProductsAllHaveImages()
        {
            var inventoryImages = _productInventoryPage.InventoryItems.Locator("img");

            await Expect(inventoryImages).ToHaveCountAsync(6);
        }

        [Test]
        public async Task ProductsAllHaveAddToCartButtons()
        {
            var addToCartButtons = _productInventoryPage.InventoryItems.Locator("button").GetByText("Add to cart");
            var expectedButtonCount = 6;

            await Expect(addToCartButtons).ToHaveCountAsync(expectedButtonCount);
        }

        [Test]
        public async Task ProductsAllHaveAPriceListed()
        {
            int validPrices = 0;
            var itemCount = _productInventoryPage.InventoryItems.AllAsync().Result.Count;
            var prices = await _productInventoryPage.InventoryItems.Locator(".inventory_item_price").AllAsync();
            foreach (var price in prices)
            {
                var priceText = await price.InnerTextAsync();
                var priceDecimal = string.Join("", priceText.Skip(1));

                if (decimal.TryParse(priceDecimal, out _))
                {
                    validPrices++;
                }
            }

            Assert.That(validPrices, Is.EqualTo(itemCount));
        }

        [Test]
        public async Task ClickingAddToCartChangesTextToRemove()
        {
            var cartButtons = await _productInventoryPage.InventoryItems.Locator("button").AllAsync();

            foreach (var cartButton in cartButtons)
            {
                await cartButton.ClickAsync();
                var buttonText = await cartButton.TextContentAsync();

                Assert.That(buttonText, Is.EqualTo("Remove"));
            }
        }

        [Test]
        public async Task ClickingAddToCartIncreasesCartItemCountByOne()
        {
            var itemCount = await _productInventoryPage.InventoryItems.CountAsync();
            var cartButtons = await _productInventoryPage.InventoryItems.Locator("button").AllAsync();

            foreach (var cartButton in cartButtons)
            {
                await cartButton.ClickAsync();
            }
            var cartItemCount = await _productInventoryPage.ShoppingCartBadge.TextContentAsync();
            
            Assert.That(cartItemCount, Is.EqualTo(itemCount.ToString()));
        }

        [Test]
        public async Task ClickingAProductTitleTakesUserToItsDetailsPage()
        {
            var firstProductTitle = _productInventoryPage.InventoryItems.First.Locator(".inventory_item_name");
            var productName = await firstProductTitle.TextContentAsync();

            await firstProductTitle.ClickAsync();
            var itemName = _page.Locator(".inventory_details_name");

            await Expect(itemName).ToHaveTextAsync(productName);
        }
    }
}
