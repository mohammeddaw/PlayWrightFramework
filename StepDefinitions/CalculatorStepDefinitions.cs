using Microsoft.Playwright;
using PlayWrightFramework.Drivers;
using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;

namespace PlayWrightFramework.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions:PageTest
    {
        private readonly PlaywrightDriver playwrightDriver;

        public CalculatorStepDefinitions(PlaywrightDriver playwrightDriver)
        {
            this.playwrightDriver = playwrightDriver;
        }


        [Given(@"the user logs onto declaration of compliance page")]
        public async Task GivenTheUserLogsOntoDeclarationOfCompliancePage()
        {
            var page = playwrightDriver.page;
            await page.GotoAsync("https://tprautoenrollstaging.crm11.dynamics.com/main.aspx");
            // Expect a title "to contain" a substring.
            await Expect(page).ToHaveTitleAsync(new Regex("Sign in to your account"));

            // create a locator
           // var getStarted = Page.GetByRole(AriaRole.Link, new() { Name = "Get started" });

            await page.GetByPlaceholder("someone@example.com").ClickAsync();
            await page.GetByPlaceholder("someone@example.com").FillAsync("testCrm.admin@tprCapita.onmicrosoft.com");
            await page.GetByPlaceholder("someone@example.com").PressAsync("Enter");
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();
            await page.Locator("#i0118").FillAsync("Reading1234");
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "No" }).ClickAsync();
            await Expect(page).ToHaveTitleAsync(new Regex("Microsoft Dynamics 365"));

            await page.GotoAsync("https://tprautoenrollstaging.crm11.dynamics.com/main.aspx?forceUCI=1&pagetype=apps");

            await page.FrameLocator("iframe[title=\"AppLandingPage\"]").GetByLabel("TPR Auto Enrolment\r\nTPR Auto Enrolment APP\r\nPublished by Default Publisher for tprautoenrollstaging.\r\nPublished on 04/05/2022.\r\nUnified Interface.\r\n12 of 12").ClickAsync();

            await page.GotoAsync("https://tprautoenrollstaging.crm11.dynamics.com/main.aspx?appid=cf6bfc83-51e0-e911-a813-000d3a7ed5a2&pagetype=dashboard&type=system&_canOverride=true");

            await page.GetByText("Organisations").ClickAsync();
         
            await page.GetByPlaceholder("Filter by keyword").ClickAsync();

            await page.GetByPlaceholder("Filter by keyword").FillAsync("Bill Ltd");

            await page.GetByPlaceholder("Filter by keyword").PressAsync("Enter");

            await page.GetByLabel("Bill Ltd").ClickAsync();

            await page.GetByText("Registered").ClickAsync();

            await Expect(page.GetByText("Registered")).ToBeVisibleAsync();


        }



    }
}