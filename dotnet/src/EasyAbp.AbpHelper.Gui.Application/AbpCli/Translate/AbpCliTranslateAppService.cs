using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos;
using EasyAbp.AbpHelper.Gui.AbpCli.Translate.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Translate
{
    public class AbpCliTranslateAppService : AbpCliAppService, IAbpCliTranslateAppService
    {
        private readonly TranslateCommand _translateCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliTranslateAppService(
            TranslateCommand translateCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _translateCommand = translateCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public virtual async Task<ServiceExecutionResult> CreateTranslationFileAsync(AbpCreateTranslationFileInput input)
        {
            var args = CreateCommandLineArgs(input, "abp translate");

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _translateCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> ApplyChangesAsync(AbpApplyChangesInput input)
        {
            var args = CreateCommandLineArgs(input, "abp translate");

            using (_currentDirectoryHelper.Change(input.RunningPath))
            {
                await _translateCommand.ExecuteAsync(args);
            }

            return new ServiceExecutionResult(true);
        }
    }
}