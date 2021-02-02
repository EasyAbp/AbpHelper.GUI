using System;

namespace EasyAbp.AbpHelper.Gui.Services.AbpCli.New.Dtos
{
    [Flags]
    public enum Database
    {
        SqlServer = 1,
        MySQL = 2,
        SQLite = 4,
        OracleDevart = 8,
        PostgreSQL = 16
    }
}