using System;
using System.ComponentModel;
using EasyAbp.AbpHelper.Gui.Shared;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos;

[Flags]
[ToStringUseDescription]
public enum ProxyType
{
    [Description("csharp")]
    CSharp = 1,
    
    [Description("ng")]
    Angular = 2,
    
    [Description("js")]
    JavaScript = 4
}