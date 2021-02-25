using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.GetSource.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.GetSource
{
    public class AbpCliGetSourceAppService : AbpCliAppService, IAbpCliGetSourceAppService
    {
        private readonly GetSourceCommand _getSourceCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliGetSourceAppService(
            GetSourceCommand getSourceCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _getSourceCommand = getSourceCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public virtual async Task<ServiceExecutionResult> GetSourceAsync(AbpGetSourceInput input)
        {
            var args = CreateCommandLineArgs(input, "abp get-source", input.ModuleName);

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _getSourceCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}