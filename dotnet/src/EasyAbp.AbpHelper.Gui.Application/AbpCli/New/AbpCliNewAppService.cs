using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New
{
    public class AbpCliNewAppService : AbpCliAppService, IAbpCliNewAppService
    {
        private readonly NewCommand _newCommand;

        public AbpCliNewAppService(NewCommand newCommand)
        {
            _newCommand = newCommand;
        }
        
        public virtual async Task<ServiceExecutionResult> CreateAppAsync(AbpNewAppInput input)
        {
            var args = CreateCommandLineArgs(input, "abp new", input.SolutionName);

            await _newCommand.ExecuteAsync(args);

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> CreateAppNoLayersAsync(AbpNewAppNoLayersInput input)
        {
            var args = CreateCommandLineArgs(input, "abp new", input.SolutionName);

            await _newCommand.ExecuteAsync(args);

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> CreateModuleAsync(AbpNewModuleInput input)
        {
            var args = CreateCommandLineArgs(input, "abp new", input.SolutionName);

            await _newCommand.ExecuteAsync(args);

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> CreateConsoleAsync(AbpNewConsoleInput input)
        {
            var args = CreateCommandLineArgs(input, "abp new", input.SolutionName);

            await _newCommand.ExecuteAsync(args);

            return new ServiceExecutionResult(true);
        }
    }
}