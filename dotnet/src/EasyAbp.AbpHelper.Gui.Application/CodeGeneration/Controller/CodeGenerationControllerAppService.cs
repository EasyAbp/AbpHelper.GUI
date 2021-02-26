using System.Threading.Tasks;
using EasyAbp.AbpHelper.Core.Commands.Generate.Controller;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Controller.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Controller
{
    public class CodeGenerationControllerAppService : CodeGenerationAppService, ICodeGenerationControllerAppService
    {
        private readonly ControllerCommand _controllerCommand;

        public CodeGenerationControllerAppService(ControllerCommand controllerCommand)
        {
            _controllerCommand = controllerCommand;
        }
        
        public virtual async Task<ServiceExecutionResult> GenerateAsync(AbpHelperGenerateControllerInput input)
        {
            await _controllerCommand.RunCommand(ObjectMapper.Map<AbpHelperGenerateControllerInput, ControllerCommandOption>(input));

            return new ServiceExecutionResult(true);
        }
    }
}