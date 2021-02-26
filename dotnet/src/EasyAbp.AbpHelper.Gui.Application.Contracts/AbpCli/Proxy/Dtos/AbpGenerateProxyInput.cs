using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos
{
    [Serializable]
    public class AbpGenerateProxyInput : InputDtoWithDirectory
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

        protected AbpGenerateProxyInput([NotNull] string directory, [CanBeNull] string module,
            [CanBeNull] string apiName, [CanBeNull] string source, [CanBeNull] string target) : base(directory)
        {
            Module = module;
            ApiName = apiName;
            Source = source;
            Target = target;
        }
    }
}