using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Services.AbpCli.New;
using EasyAbp.AbpHelper.Gui.Services.AbpCli.New.Dtos;
using EasyAbp.AbpHelper.Gui.Services.Shared.Dtos;
using EasyAbp.AbpHelper.Gui.Shared;
using Volo.Abp.Cli.Args;
using Volo.Abp.Cli.Commands;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.AbpCli
{
    public class AbpCliNewService : AbpCliServiceBase, IAbpCliNewService, ITransientDependency
    {
        private readonly NewCommand _newCommand;

        public AbpCliNewService(NewCommand newCommand)
        {
            _newCommand = newCommand;
        }
        
        public virtual async Task<ServiceExecutionResult> CreateAppAsync(AbpNewAppInput input)
        {
            var args = CreateCommandLineArgs(input, "abp new", input.SolutionName);

            await _newCommand.ExecuteAsync(args);

            return new ServiceExecutionResult(true);
        }
    }
}