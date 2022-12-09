using Volo.Abp.Cli;

namespace EasyAbp.AbpHelper.Gui.Blazor;

public static class AbpVersionHelper
{
    public static readonly string AbpVersion = typeof(AbpCliCoreModule).Assembly.GetName().Version?.ToString(3);

    public static readonly string AbpHelperGuiVersion = typeof(GuiBlazorModule).Assembly.GetName().Version?.ToString(3);
}