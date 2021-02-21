using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using EasyAbp.AbpHelper.Gui.Solutions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var application = builder.AddApplication<GuiBlazorModule>(options =>
            {
                options.UseAutofac();
            });

            var host = builder.Build();

            await application.InitializeAsync(host.Services);

            await SetCurrentSolutionAsync(host.Services);

            await host.RunAsync();
        }
        
        private static async Task SetCurrentSolutionAsync(IServiceProvider services)
        {
            var currentSolution = services.GetRequiredService<ICurrentSolution>();
            var solutionAppService = services.GetRequiredService<ISolutionAppService>();

            var solutions = await solutionAppService.GetListAsync();

            currentSolution.Set(solutions.Items.Count > 0 ? solutions.Items[0] : null);
        }
    }
}
