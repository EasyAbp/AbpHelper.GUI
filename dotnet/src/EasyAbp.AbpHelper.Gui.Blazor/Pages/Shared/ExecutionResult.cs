using System;

namespace EasyAbp.AbpHelper.Gui.Pages.Shared
{
    [Flags]
    public enum ExecutionResult
    {
        Pending = 1,
        Success = 2,
        Failure = 4
    }
}