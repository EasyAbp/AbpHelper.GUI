using System.Threading.Tasks;
using EasyAbp.AbpHelper.Core.Commands.Generate.Localization;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Localization;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Localization.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Localization
{
    public class CodeGenerationLocalizationAppService : CodeGenerationAppService, ICodeGenerationLocalizationAppService
    {
        private readonly LocalizationCommand _localizationCommand;

        public CodeGenerationLocalizationAppService(LocalizationCommand localizationCommand)
        {
            _localizationCommand = localizationCommand;
        }

        public virtual async Task<ServiceExecutionResult> GenerateItemsAsync(AbpHelperGenerateLocalizationItemsInput input)
        {
            await _localizationCommand.RunCommand(ObjectMapper.Map<AbpHelperGenerateLocalizationItemsInput, LocalizationCommandOption>(input));

            return new ServiceExecutionResult(true);
        }
    }
}