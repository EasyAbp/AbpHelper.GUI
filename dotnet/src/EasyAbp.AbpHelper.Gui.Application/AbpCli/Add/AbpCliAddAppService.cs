using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Add.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Add
{
    public class AbpCliAddAppService : AbpCliAppService, IAbpCliAddAppService
    {
        private readonly AddPackageCommand _addPackageCommand;
        private readonly AddModuleCommand _addModuleCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliAddAppService(
            AddPackageCommand addPackageCommand,
            AddModuleCommand addModuleCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _addPackageCommand = addPackageCommand;
            _addModuleCommand = addModuleCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public async Task<ServiceExecutionResult> AddPackageAsync(AbpAddPackageInput input)
        {
            var args = CreateCommandLineArgs(input, "abp add-package", input.PackageName);

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _addPackageCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }

        public async Task<ServiceExecutionResult> AddModuleAsync(AbpAddModuleInput input)
        {
            var args = CreateCommandLineArgs(input, "abp add-module", input.ModuleName);

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _addModuleCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}