using System;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    [Flags]
    public enum AppUiFramework
    {
        None = 1,
        Mvc = 2,
        Angular = 4,
        Blazor = 8,
        BlazorServer = 16,
        MauiBlazor = 32
    }
}