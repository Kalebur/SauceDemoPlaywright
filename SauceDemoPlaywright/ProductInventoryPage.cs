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
    }
}
