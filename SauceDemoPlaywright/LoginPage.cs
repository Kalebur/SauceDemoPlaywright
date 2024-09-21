using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace SauceDemoPlaywright
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
            _mainHeading = page.Locator("text=Swag Labs");
        }
        public string Url = "https://www.saucedemo.com/";

        private readonly ILocator _mainHeading;
    }
}
