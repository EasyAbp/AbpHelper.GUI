using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos
{
    [Serializable]
    public class AbpGenerateRemoveJavaScriptProxyInput : InputDtoWithDirectory
    {
        public virtual ProxyType Type => ProxyType.JavaScript;

        [Required]
        [NotNull]
        public virtual string Url { get; set; }

        public virtual ServiceType ServiceType { get; set; }

        [CanBeNull]
        public virtual string Module { get; set; }

        [CanBeNull]
        public virtual string Output { get; set; }

        public AbpGenerateRemoveJavaScriptProxyInput()
        {
        }

        public AbpGenerateRemoveJavaScriptProxyInput([NotNull] string url, ServiceType serviceType,
            [CanBeNull] string module, [CanBeNull] string output)
        {
            Url = url;
            ServiceType = serviceType;
            Module = module;
            Output = output;
        }
    }
}