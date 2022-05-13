using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Clean.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Clean
{
    public class AbpCliCleanAppService : AbpCliAppService, IAbpCliCleanAppService
    {
        private readonly CleanCommand _cleanCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliCleanAppService(
            CleanCommand cleanCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _cleanCommand = cleanCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public virtual async Task<ServiceExecutionResult> CleanAsync(AbpCleanInput input)
        {
            var args = CreateCommandLineArgs(input, "abp clean");

            using (_currentDirectoryHelper.Change(input.Directory))
            {
                await _cleanCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}