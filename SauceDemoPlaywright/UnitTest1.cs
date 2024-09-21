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

    [SetUp]
    public async Task Setup()
    {
        _page = await Browser.NewPageAsync();
        _loginPage = new LoginPage(_page);
    }

    [Test]
    public async Task HasTitle()
    {
        await Page.GotoAsync("https://playwright.dev");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
    }

    [Test]
    public async Task GetStartedLink()
    {
        await Page.GotoAsync("https://playwright.dev");

        // Click the get started link.
        await Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

        // Expects page to have a heading with the name of Installation.
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
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
        await _loginPage.LoginAs("standard_user");

        await Expect(_page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
    }
}