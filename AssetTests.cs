using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SnipeItTest.Helpers;
using SnipeItTest.Pages;

namespace SnipeItTest;

[TestFixture]
public class AssetTests : PageTest
{
    private LoginPage _loginPage = null!;
    private CreateAssetPage _createAssetPage = null!;
    private AssetsListPage _assetsListPage = null!;
    private AssetDetailPage _assetDetailPage = null!;
    private TestLogger _logger = null!;
    private string _username = string.Empty;
    private string _password = string.Empty;

    private readonly string _assetTag = $"TEST-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
    private string _checkedOutTo = string.Empty;

    [SetUp]
    public void SetUp()
    {
        // read credentials from LoginData.txt
        var lines = File.ReadAllLines("LoginData.txt");
        _username = lines[0].Trim();
        _password = lines[1].Trim();

        // initialise page objects and logger
        _loginPage = new LoginPage(Page);
        _createAssetPage = new CreateAssetPage(Page);
        _assetsListPage = new AssetsListPage(Page);
        _assetDetailPage = new AssetDetailPage(Page);
        _logger = new TestLogger();
    }

    [TearDown]
    public void TearDown()
    {
        // save log
        _logger.Save();
    }

    [Test]
    public async Task FullAssetFlow()
    {
        _logger.Log("Test started");
        _logger.Log($"Asset tag generated: {_assetTag}");
        _logger.LogSeparator();

        // Step 1: Login
        await _loginPage.GoToAsync();
        await _loginPage.LoginAsync(_username, _password);

        Assert.That(Page.Url, Does.Not.Contain("/login"));
        var msg = await Page.Locator("text=You have successfully logged in.").IsVisibleAsync();
        Assert.That(msg, Is.True);

        _logger.Log("Step 1 PASSED: Login successful");
        _logger.Log($"  URL after login: {Page.Url}");
        _logger.LogSeparator();

        // Step 2: Create Asset
        await _createAssetPage.GoToAsync();
        _checkedOutTo = await _createAssetPage.CreateAssetAsync(_assetTag);

        Assert.That(Page.Url, Does.Not.Contain("/hardware/create"));
        Assert.That(_checkedOutTo, Is.Not.Empty);

        _logger.Log("Step 2 PASSED: Asset created successfully");
        _logger.Log($"  Asset Tag:     {_assetTag}");
        _logger.Log($"  Model:         MacBook Pro 13\"");
        _logger.Log($"  Status:        Ready to Deploy");
        _logger.Log($"  Checked Out To: {_checkedOutTo}");
        _logger.Log($"  URL after save: {Page.Url}");
        _logger.LogSeparator();

        // Step 3: Find the asset
        await _assetsListPage.GoToAsync();
        var found = await _assetsListPage.AssetExistsAsync(_assetTag);

        Assert.That(found, Is.True, $"Asset {_assetTag} should appear in the list");

        _logger.Log("Step 3 PASSED: Asset found in the assets list");
        _logger.Log($"  Searched for: {_assetTag}");
        _logger.LogSeparator();

        // Step 4: Open the asset and validate its details
        await _assetsListPage.OpenAssetAsync(_assetTag);
        await _assetDetailPage.WaitForLoadAsync();

        var assetTag = await _assetDetailPage.GetAssetTagAsync();
        var status = await _assetDetailPage.GetStatusAsync();
        var model = await _assetDetailPage.GetModelAsync();
        var breadcrumb = await _assetDetailPage.GetBreadcrumbModelAsync();
        var checkedOut = await _assetDetailPage.GetCheckedOutToAsync();

        Assert.That(assetTag, Is.EqualTo(_assetTag));
        Assert.That(status, Does.Contain("Ready to Deploy"));
        Assert.That(model, Does.Contain("MacBook Pro").Or.Contain("Macbook Pro"));
        Assert.That(breadcrumb, Does.Contain("MacBook Pro").Or.Contain("Macbook Pro"));
        Assert.That(_checkedOutTo, Does.Contain(checkedOut));

        _logger.Log("Step 4 PASSED: Asset detail page validated");
        _logger.Log($"  Asset Tag:      {assetTag}");
        _logger.Log($"  Model:          {model}");
        _logger.Log($"  Breadcrumb:     {breadcrumb}");
        _logger.Log($"  Status:         {status}");
        _logger.Log($"  Checked Out To: {checkedOut}");
        _logger.Log($"  Asset URL:      {Page.Url}");
        _logger.LogSeparator();

        // Step 5: Validate History
        await _assetDetailPage.OpenHistoryTabAsync();

        Assert.That(await _assetDetailPage.HistoryIsNotEmptyAsync(), Is.True, "History should not be empty");
        Assert.That(await _assetDetailPage.HistoryContainsActionAsync("create"), Is.True, "History should contain a create action");
        Assert.That(await _assetDetailPage.HistoryContainsActionAsync("checkout"), Is.True, "History should contain a checkout action");

        _logger.Log("Step 5 PASSED: History tab validated");
        _logger.Log("  History contains: create action");
        _logger.Log("  History contains: checkout action");
        _logger.LogSeparator();

        _logger.Log("All steps passed.");
    }
}