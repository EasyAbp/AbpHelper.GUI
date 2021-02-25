using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos;
using EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Switch
{
    public class AbpCliSwitchAppService : AbpCliAppService, IAbpCliSwitchAppService
    {
        private readonly SwitchToPreviewCommand _switchToPreviewCommand;
        private readonly SwitchToNightlyCommand _switchToNightlyCommand;
        private readonly SwitchToStableCommand _switchToStableCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliSwitchAppService(
            SwitchToPreviewCommand switchToPreviewCommand,
            SwitchToNightlyCommand switchToNightlyCommand,
            SwitchToStableCommand switchToStableCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _switchToPreviewCommand = switchToPreviewCommand;
            _switchToNightlyCommand = switchToNightlyCommand;
            _switchToStableCommand = switchToStableCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public virtual async Task<ServiceExecutionResult> SwitchToPreviewAsync(AbpSwitchToPreviewInput input)
        {
            var args = CreateCommandLineArgs(input, "abp switch-to-preview");

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _switchToPreviewCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> SwitchToNightlyAsync(AbpSwitchToNightlyInput input)
        {
            var args = CreateCommandLineArgs(input, "abp switch-to-nightly");

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _switchToNightlyCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> SwitchToStableAsync(AbpSwitchToStableInput input)
        {
            var args = CreateCommandLineArgs(input, "abp switch-to-stable");

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _switchToStableCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}