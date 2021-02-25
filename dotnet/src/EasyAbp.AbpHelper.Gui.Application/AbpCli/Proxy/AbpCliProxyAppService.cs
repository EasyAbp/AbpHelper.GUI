using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.GetSource;
using EasyAbp.AbpHelper.Gui.AbpCli.GetSource.Dtos;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy
{
    public class AbpCliProxyAppService : AbpCliAppService, IAbpCliProxyAppService
    {
        private readonly GenerateProxyCommand _generateProxyCommand;
        private readonly RemoveProxyCommand _removeProxyCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliProxyAppService(
            GenerateProxyCommand generateProxyCommand,
            RemoveProxyCommand removeProxyCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _generateProxyCommand = generateProxyCommand;
            _removeProxyCommand = removeProxyCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public virtual async Task<ServiceExecutionResult> GenerateProxyAsync(AbpGenerateProxyInput input)
        {
            var args = CreateCommandLineArgs(input, "abp generate-proxy");

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _generateProxyCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> RemoveProxyAsync(AbpRemoveProxyInput input)
        {
            var args = CreateCommandLineArgs(input, "abp remove-proxy");

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _removeProxyCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}