using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace SauceDemoPlaywright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTest : PageTest
{
    private LoginPage _loginPage;
    private IPage _page;
    private SauceDemoHelpers _sauceDemoHelpers;

    [SetUp]
    public async Task Setup()
    {
        _page = await Browser.NewPageAsync();
        _loginPage = new LoginPage(_page);
        _sauceDemoHelpers = new SauceDemoHelpers(_page, _loginPage, new ProductInventoryPage(_page));
    }

    [Test]
    public async Task HasHeading()
    {
        await _loginPage.GotoAsync();

        await Expect(_loginPage.MainHeading).ToBeVisibleAsync();
    }

    [Test]
    public async Task CanLogin()
    {
        await _sauceDemoHelpers.LoginAs("standard_user");
        await Expect(_page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
    }
}