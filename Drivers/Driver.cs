using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWrightFramework.Drivers
{
    public class PlaywrightDriver : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;
        public PlaywrightDriver()
        {
            _page = Task.Run(InitializePlayWright);
        }

        public IPage page => _page.Result;



        public void Dispose()
        {
            _browser.CloseAsync();
        }

        public async Task<IPage> InitializePlayWright()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                //SlowMo = 1000
            });

            var context = await _browser.NewContextAsync(new()
            {
                RecordVideoDir = "videos/"

            });

            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true,

            });
            return await _browser.NewPageAsync();

        }
    }
}
