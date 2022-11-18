using System;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    [Flags]
    public enum AppMobileApplicationFramework
    {
        None = 1,
        ReactNative = 2,
        Maui = 4
    }
}