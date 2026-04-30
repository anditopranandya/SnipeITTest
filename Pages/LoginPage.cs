using Microsoft.Playwright;
namespace SnipeItTest.Pages;

public class LoginPage(IPage page)
{
    public async Task GoToAsync()
    {
        // navigate to the login page
        await page.GotoAsync("https://demo.snipeitapp.com/login");
    }

    public async Task LoginAsync(string username, string password)
    {
        // fill in the username and password fields
        await page.FillAsync("#username", username);
        await page.FillAsync("#password-field", password);

        // click on the login button
        await page.ClickAsync("button[type='submit']");

        // wait until the URL is no longer /login
        await page.WaitForURLAsync(url => !url.Contains("/login"));
    }
}