using System;
using System.Collections.Generic;
using System.ComponentModel;
using EasyAbp.AbpHelper.Gui.Shared;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    [Flags]
    [ToStringUseDescription]
    public enum Database
    {
        [Description("SqlServer")]
        SqlServer = 1,
        
        [Description("MySQL")]
        MySQL = 2,
        
        [Description("SQLite")]
        SQLite = 4,
        
        [Description("Oracle")]
        Oracle = 8,
        
        [Description("Oracle-Devart")]
        OracleDevart = 16,
        
        [Description("PostgreSQL")]
        PostgreSQL = 32
    }
}