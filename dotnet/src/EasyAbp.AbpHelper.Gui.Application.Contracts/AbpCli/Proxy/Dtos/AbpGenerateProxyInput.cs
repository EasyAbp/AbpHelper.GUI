using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos
{
    [Serializable]
    public class AbpGenerateProxyInput : InputDtoWithRunningPath
    {
        [CanBeNull]
        public virtual string Module { get; set; }
        
        [CanBeNull]
        public virtual string ApiName { get; set; }
        
        [CanBeNull]
        public virtual string Source { get; set; }
        
        [CanBeNull]
        public virtual string Target { get; set; }

        public AbpGenerateProxyInput()
        {
        }

        protected AbpGenerateProxyInput([NotNull] string runningPath, [CanBeNull] string module,
            [CanBeNull] string apiName, [CanBeNull] string source, [CanBeNull] string target) : base(runningPath)
        {
            Module = module;
            ApiName = apiName;
            Source = source;
            Target = target;
        }
    }
}