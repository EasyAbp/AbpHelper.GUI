using EasyAbp.AbpHelper.Gui.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.AbpHelper.Gui.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class GuiController : AbpControllerBase
    {
        protected GuiController()
        {
            LocalizationResource = typeof(GuiResource);
        }
    }
}