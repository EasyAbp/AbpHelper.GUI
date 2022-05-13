using System.Threading.Tasks;
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

        public virtual Task<ServiceExecutionResult> GenerateAngularProxyAsync(AbpGenerateRemoveAngularProxyInput input)
        {
            return GenerateProxyAsync(input);
        }

        public virtual Task<ServiceExecutionResult> RemoveAngularProxyAsync(AbpGenerateRemoveAngularProxyInput input)
        {
            return RemoveProxyAsync(input);
        }

        public virtual Task<ServiceExecutionResult> GenerateCSharpProxyAsync(AbpGenerateRemoveCSharpProxyInput input)
        {
            return GenerateProxyAsync(input);
        }

        public virtual Task<ServiceExecutionResult> RemoveCSharpProxyAsync(AbpGenerateRemoveCSharpProxyInput input)
        {
            return RemoveProxyAsync(input);
        }

        public virtual Task<ServiceExecutionResult> GenerateJavaScriptProxyAsync(AbpGenerateRemoveJavaScriptProxyInput input)
        {
            return GenerateProxyAsync(input);
        }

        public virtual Task<ServiceExecutionResult> RemoveJavaScriptProxyAsync(AbpGenerateRemoveJavaScriptProxyInput input)
        {
            return RemoveProxyAsync(input);
        }

        protected virtual async Task<ServiceExecutionResult> GenerateProxyAsync(InputDtoWithDirectory input)
        {
            var args = CreateCommandLineArgs(input, "abp generate-proxy");

            using (_currentDirectoryHelper.Change(input.Directory))
            {
                await _generateProxyCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }

        protected virtual async Task<ServiceExecutionResult> RemoveProxyAsync(InputDtoWithDirectory input)
        {
            var args = CreateCommandLineArgs(input, "abp remove-proxy");

            using (_currentDirectoryHelper.Change(input.Directory))
            {
                await _removeProxyCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}