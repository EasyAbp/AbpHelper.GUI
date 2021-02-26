using System.Threading.Tasks;
using EasyAbp.AbpHelper.Core.Commands.Generate.Methods;
using EasyAbp.AbpHelper.Core.Commands.Generate.Service;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.AppService
{
    public class CodeGenerationAppServiceAppService : CodeGenerationAppService, ICodeGenerationAppServiceAppService
    {
        private readonly ServiceCommand _serviceCommand;
        private readonly MethodsCommand _methodsCommand;

        public CodeGenerationAppServiceAppService(
            ServiceCommand serviceCommand,
            MethodsCommand methodsCommand)
        {
            _serviceCommand = serviceCommand;
            _methodsCommand = methodsCommand;
        }

        public virtual async Task<ServiceExecutionResult> GenerateClassAsync(AbpHelperGenerateAppServiceClassInput input)
        {
            await _serviceCommand.RunCommand(ObjectMapper.Map<AbpHelperGenerateAppServiceClassInput, ServiceCommandOption>(input));

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> GenerateMethodsAsync(AbpHelperGenerateAppServiceMethodsInput input)
        {
            await _methodsCommand.RunCommand(ObjectMapper.Map<AbpHelperGenerateAppServiceMethodsInput, MethodsCommandOption>(input));

            return new ServiceExecutionResult(true);
        }
    }
}