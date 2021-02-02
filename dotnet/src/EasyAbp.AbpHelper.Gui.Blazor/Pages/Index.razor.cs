using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Services.AbpCli.New;
using EasyAbp.AbpHelper.Gui.Services.AbpCli.New.Dtos;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages
{
    public partial class Index
    {
        protected IAbpCliNewService Service { get; set; }

        public Index(IAbpCliNewService service)
        {
            Service = service;
        }
        
        public async Task ExecuteAsync(AbpNewAppInput input)
        {
            await Service.CreateAppAsync(input);
        }
    }
}
