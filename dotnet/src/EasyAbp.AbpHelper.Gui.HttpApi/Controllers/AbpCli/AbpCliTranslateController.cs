using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Translate;
using EasyAbp.AbpHelper.Gui.AbpCli.Translate.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/translate")]
    public class AbpCliTranslateController : GuiController, IAbpCliTranslateAppService
    {
        private readonly IAbpCliTranslateAppService _service;

        public AbpCliTranslateController(IAbpCliTranslateAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("create-translation-file")]
        public Task<ServiceExecutionResult> CreateTranslationFileAsync(AbpCreateTranslationFileInput input)
        {
            return _service.CreateTranslationFileAsync(input);
        }

        [HttpPost]
        [Route("apply-changes")]
        public Task<ServiceExecutionResult> ApplyChangesAsync(AbpApplyChangesInput input)
        {
            return _service.ApplyChangesAsync(input);
        }
    }
}