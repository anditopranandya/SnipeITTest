using Microsoft.Playwright;

namespace SnipeItTest.Pages;

public class CreateAssetPage(IPage page)
{
    public async Task GoToAsync()
    {
        // navigate to the create asset form
        await page.ClickAsync("a.dropdown-toggle[data-toggle='dropdown']:has-text('Create New')");
        await page.ClickAsync("a[href='https://demo.snipeitapp.com/hardware/create']");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task<string> CreateAssetAsync(string assetTag)
    {
        // fill in the asset tag
        await page.FillAsync("#asset_tag", assetTag);

        // select MacBook Pro 13" from the model dropdown
        await page.ClickAsync("#select2-model_select_id-container");
        await page.WaitForSelectorAsync(".select2-search__field");
        await page.FillAsync(".select2-search__field", "MacBook Pro 13");
        await page.WaitForSelectorAsync(".select2-results__option:not(.loading-results)");
        await page.ClickAsync(".select2-results__option:first-child");

        // select "Ready to Deploy" status
        await page.SelectOptionAsync("#status_select_id", "1");

        // pick a random user from the checkout dropdown
        await page.ClickAsync("#select2-assigned_user_select-container");
        await page.WaitForSelectorAsync(".select2-search__field");
        await page.FillAsync(".select2-search__field", " ");
        await page.WaitForSelectorAsync(".select2-results__option:not(.loading-results)");

        // get all available users and pick one randomly
        var users = await page.QuerySelectorAllAsync(".select2-results__option");
        var randomUser = users[new Random().Next(users.Count)];
        var userName = await randomUser.InnerTextAsync();
        await randomUser.ClickAsync();

        // save the asset
        await page.ClickAsync("#submit_button");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // return the selected username for later verifications
        return userName.Trim();
    }
}
