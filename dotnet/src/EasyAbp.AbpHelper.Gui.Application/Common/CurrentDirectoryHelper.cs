using System;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Common
{
    public class CurrentDirectoryHelper : ICurrentDirectoryHelper, ITransientDependency
    {
        public IDisposable Change(string path)
        {
            var originalPath = Environment.CurrentDirectory;
            
            Environment.CurrentDirectory = path;
            
            return new DisposeAction(() =>
            {
                Environment.CurrentDirectory = originalPath;
            });
        }
    }
}