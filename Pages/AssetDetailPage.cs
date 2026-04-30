using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SnipeItTest.Pages;

public class AssetDetailPage(IPage page)
{
    public async Task WaitForLoadAsync()
    {
        // wait for the details tab to be visible
        await page.WaitForSelectorAsync("#details.active");
    }

    public async Task<string> GetAssetTagAsync()
    {
        // asset tag value is inside a span with this class
        return (await page.Locator(".js-copy-asset_tag").InnerTextAsync()).Trim();
    }

    public async Task<string> GetStatusAsync()
    {
        // status link always points to /statuslabels/
        return (await page.Locator("#details .well").First.InnerTextAsync()).Trim();
    }

    public async Task<string> GetModelAsync()
    {
        // model name shown in the detail panel
        return (await page.Locator(".js-copy-asset_model").InnerTextAsync()).Trim();
    }

    public async Task<string> GetBreadcrumbModelAsync()
    {
        // model name also appears in the breadcrumb at the top of the page
        return (await page.Locator("li.breadcrumb-item.active").InnerTextAsync()).Trim();
    }

    public async Task<string> GetCheckedOutToAsync()
    {
        // the checked out user's name is a link inside the well div
        return (await page.Locator("#details .well a[href*='users']").InnerTextAsync()).Trim();
    }

    public async Task OpenHistoryTabAsync()
    {
        // click the History tab
        await page.ClickAsync("a[href='#history']");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.WaitForTimeoutAsync(2000);
    }

    public async Task<bool> HistoryContainsActionAsync(string action)
    {
        // check if any row in the history table contains the given action text
        return await page.Locator($"#historyListingTable tbody tr:has-text('{action}')").First.IsVisibleAsync();
    }

    public async Task<bool> HistoryIsNotEmptyAsync()
    {
        // check that the table has at least one real row
        var noRecords = await page.Locator("#historyListingTable tbody tr.no-records-found").IsVisibleAsync();
        return !noRecords;
    }
}
