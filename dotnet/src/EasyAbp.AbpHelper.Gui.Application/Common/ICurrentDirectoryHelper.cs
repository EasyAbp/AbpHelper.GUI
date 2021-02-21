using System;

namespace EasyAbp.AbpHelper.Gui.Common
{
    public interface ICurrentDirectoryHelper
    {
        IDisposable Change(string path);
    }
}