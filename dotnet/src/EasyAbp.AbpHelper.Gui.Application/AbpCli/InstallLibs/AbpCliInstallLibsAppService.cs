using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.InstallLibs.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.InstallLibs
{
    public class AbpCliInstallLibsAppService : AbpCliAppService, IAbpCliInstallLibsAppService
    {
        private readonly InstallLibsCommand _installLibsCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliInstallLibsAppService(
            InstallLibsCommand installLibsCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _installLibsCommand = installLibsCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public virtual async Task<ServiceExecutionResult> InstallLibsAsync(AbpInstallLibsInput input)
        {
            var args = CreateCommandLineArgs(input, "abp install-libs");

            using (_currentDirectoryHelper.Change(input.Directory))
            {
                await _installLibsCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}