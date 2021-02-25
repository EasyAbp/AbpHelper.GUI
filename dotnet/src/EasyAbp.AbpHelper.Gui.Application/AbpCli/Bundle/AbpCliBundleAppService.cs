using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Bundle.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Bundle
{
    public class AbpCliBundleAppService : AbpCliAppService, IAbpCliBundleAppService
    {
        private readonly BundleCommand _bundleCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliBundleAppService(
            BundleCommand bundleCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _bundleCommand = bundleCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public virtual async Task<ServiceExecutionResult> RunAsync(AbpBundleInput input)
        {
            var args = CreateCommandLineArgs(input, "abp bundle");

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _bundleCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}