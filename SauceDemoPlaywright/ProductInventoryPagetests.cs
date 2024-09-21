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

        [SetUp]
        public async Task Setup()
        {
            _page = await Browser.NewPageAsync();
            _loginPage = new LoginPage(_page);
            _productInventoryPage = new ProductInventoryPage(_page);
            await _loginPage.LoginAs("standard_user");
        }

        [Test]
        public async Task HasAtLeastOneInventoryItemOnPage()
        {
            var items = _page.Locator(".inventory_item");

            await Expect(items).ToHaveCountAsync(6);
        }
    }
}
