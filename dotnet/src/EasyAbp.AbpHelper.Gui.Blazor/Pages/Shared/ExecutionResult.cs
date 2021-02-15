using System;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Shared
{
    [Flags]
    public enum ExecutionResult
    {
        Pending = 1,
        Success = 2,
        Failure = 4
    }
}