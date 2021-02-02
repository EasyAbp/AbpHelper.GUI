using System;

namespace EasyAbp.AbpHelper.Gui.Services.AbpCli.New.Dtos
{
    [Flags]
    public enum AppDatabaseProvider
    {
        Ef = 1,
        Mongodb = 2
    }
}