using Microsoft.Playwright;

namespace SauceDemoPlaywright
{
    public class ProductInventoryPage
    {
        private readonly IPage _page;

        public ProductInventoryPage(IPage page)
        {
            _page = page;
        }

        public ILocator HamburgerMenu => _page.Locator("#react-burger-menu-btn");
        public ILocator LogoutLink => _page.Locator("#logout_sidebar_link");
        public ILocator HamburgerMenuCloseButton => _page.Locator("#react-burger-cross-btn");
    }
}
