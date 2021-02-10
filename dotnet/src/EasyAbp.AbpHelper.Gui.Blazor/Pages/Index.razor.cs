using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;

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
