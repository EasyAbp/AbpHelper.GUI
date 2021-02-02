using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Services.AbpCli.New;
using EasyAbp.AbpHelper.Gui.Services.AbpCli.New.Dtos;
using Volo.Abp.AspNetCore.Components.Messages;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.New
{
    public partial class CreateApp
    {
        private readonly IAbpCliNewService _service;
        
        protected AbpNewAppInput Input { get; set; } = new ();
        

        public CreateApp(
            IAbpCliNewService service,
            IUiMessageService uiMessageService) : base(uiMessageService)
        {
            _service = service;
        }

        protected override async Task ExecuteInternalAsync()
        {
            await _service.CreateAppAsync(Input);
        }
    }
}
