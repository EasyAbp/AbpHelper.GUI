using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Build;
using EasyAbp.AbpHelper.Gui.AbpCli.Build.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Build
{
    public class AbpCliBuildAppService : AbpCliAppService, IAbpCliBuildAppService
    {
        private readonly BuildCommand _buildCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliBuildAppService(
            BuildCommand buildCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _buildCommand = buildCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public virtual async Task<ServiceExecutionResult> BuildAsync(AbpBuildInput input)
        {
            var args = CreateCommandLineArgs(input, "abp build");

            using (_currentDirectoryHelper.Change(input.Directory))
            {
                await _buildCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}