using Microsoft.Playwright;
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
            await _productInventoryPage.HamburgerMenuOpenButton.ClickAsync();
            await _productInventoryPage.LogoutLink.ClickAsync();

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
    }
}
