using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace SauceDemoPlaywright
{
    public class LoginPage
    {
        public readonly string Url = "https://www.saucedemo.com/";
        private readonly IPage _page;
        public ILocator MainHeading { get; set; }
        public ILocator UsernameTextbox => _page.Locator("#user-name");
        public ILocator PasswordTextbox => _page.Locator("#password");
        public ILocator LoginButton => _page.Locator("#login-button");

        public LoginPage(IPage page)
        {
            _page = page;
            MainHeading = _page.Locator("text=Swag Labs");
        }


        public async Task GotoAsync()
        {
            await _page.GotoAsync(Url);
        }
    }
}
