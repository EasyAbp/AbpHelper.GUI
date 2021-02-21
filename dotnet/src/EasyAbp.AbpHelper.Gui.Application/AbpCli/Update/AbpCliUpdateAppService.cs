using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Update.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Update
{
    public class AbpCliUpdateAppService : AbpCliAppService, IAbpCliUpdateAppService
    {
        private readonly UpdateCommand _updateCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliUpdateAppService(
            UpdateCommand updateCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _updateCommand = updateCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public async Task<ServiceExecutionResult> UpdateAsync(AbpUpdateInput input)
        {
            var args = CreateCommandLineArgs(input, "abp update");

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _updateCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}