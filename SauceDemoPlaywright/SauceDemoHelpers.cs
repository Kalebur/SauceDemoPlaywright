using Microsoft.Playwright;

namespace SauceDemoPlaywright
{
    public class SauceDemoHelpers
    {
        private readonly IPage _page;
        private readonly LoginPage _loginPage;
        private readonly ProductInventoryPage _productInventoryPage;

        public SauceDemoHelpers(IPage page, LoginPage loginPage, ProductInventoryPage productInventoryPage)
        {
            _page = page;
            _loginPage = loginPage;
            _productInventoryPage = productInventoryPage;
        }

        public async Task LoginAs(string username)
        {
            await _loginPage.GotoAsync();
            await _loginPage.UsernameTextbox.FillAsync(username);
            await _loginPage.PasswordTextbox.FillAsync("secret_sauce");
            await _loginPage.LoginButton.ClickAsync();
        }
    }
}
