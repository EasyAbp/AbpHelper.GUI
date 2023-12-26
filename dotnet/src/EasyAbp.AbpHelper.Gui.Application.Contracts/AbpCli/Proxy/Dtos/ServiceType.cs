using System;
using System.ComponentModel;
using EasyAbp.AbpHelper.Gui.Shared;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos;

[Flags]
[ToStringUseDescription]
public enum ServiceType
{
    [Description("all")]
    All = 1,
    
    [Description("integration")]
    Integration = 2,
    
    [Description("application")]
    Application = 4
}