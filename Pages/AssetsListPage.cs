using Microsoft.Playwright;

namespace SnipeItTest.Pages;

public class AssetsListPage(IPage page)
{
    public async Task GoToAsync()
    {
        // navigate to the asset list page
        await page.ClickAsync("a[href='https://demo.snipeitapp.com/hardware']");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task<bool> AssetExistsAsync(string assetTag)
    {
        // type the asset tag into the search box
        await page.FillAsync(".search-input", assetTag);

        // press Enter to trigger the search
        await page.Keyboard.PressAsync("Enter");

        // wait for the table to reload via AJAX
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.WaitForTimeoutAsync(4000);

        // check if any row in the table contains our asset tag
        return await page.Locator($"#assetsListingTable tbody tr:has-text('{assetTag}')").First.IsVisibleAsync();
    }

    public async Task OpenAssetAsync(string assetTag)
    {
        // click the asset tag link in the matching row
        await page.Locator($"table tbody tr:has-text('{assetTag}') a").First.ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
}