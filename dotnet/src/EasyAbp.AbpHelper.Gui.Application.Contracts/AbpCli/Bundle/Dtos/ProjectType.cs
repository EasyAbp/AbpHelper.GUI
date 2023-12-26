using System;
using System.ComponentModel;
using EasyAbp.AbpHelper.Gui.Shared;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Bundle.Dtos;

[Flags]
[ToStringUseDescription]
public enum ProjectType
{
    [Description("webassembly")]
    WebAssembly = 1,

    [Description("maui-blazor")]
    MauiBlazor = 2,
}