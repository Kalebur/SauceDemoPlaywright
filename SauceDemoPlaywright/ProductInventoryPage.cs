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

        public ILocator HamburgerMenuOpenButton => _page.Locator("#react-burger-menu-btn");
        public ILocator HamburgerMenuCloseButton => _page.Locator("#react-burger-cross-btn");
        public ILocator LogoutLink => _page.Locator("#logout_sidebar_link");
        public ILocator InventoryItems => _page.Locator(".inventory_item");
        public ILocator ShoppingCartBadge => _page.Locator(".shopping_cart_badge");
    }
}
