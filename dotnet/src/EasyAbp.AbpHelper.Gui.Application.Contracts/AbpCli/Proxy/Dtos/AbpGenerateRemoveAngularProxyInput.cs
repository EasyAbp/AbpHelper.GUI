using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos
{
    [Serializable]
    public class AbpGenerateRemoveAngularProxyInput : InputDtoWithDirectory
    {
        public virtual ProxyType Type => ProxyType.Angular;

        [CanBeNull]
        public virtual string Module { get; set; }

        [CanBeNull]
        public virtual string ApiName { get; set; }

        [CanBeNull]
        public virtual string Source { get; set; }

        [CanBeNull]
        public virtual string Target { get; set; }

        [CanBeNull]
        public virtual string EntryPoint { get; set; }

        [CanBeNull]
        public virtual string Url { get; set; }

        public virtual ServiceType ServiceType { get; set; }

        public AbpGenerateRemoveAngularProxyInput()
        {
        }

        public AbpGenerateRemoveAngularProxyInput([CanBeNull] string module, [CanBeNull] string apiName,
            [CanBeNull] string source, [CanBeNull] string target, [CanBeNull] string entryPoint, [CanBeNull] string url,
            ServiceType serviceType)
        {
            Module = module;
            ApiName = apiName;
            Source = source;
            Target = target;
            EntryPoint = entryPoint;
            Url = url;
            ServiceType = serviceType;
        }
    }
}