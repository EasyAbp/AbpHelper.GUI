using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos
{
    [Serializable]
    public class AbpGenerateRemoveCSharpProxyInput : InputDtoWithDirectory
    {
        public virtual ProxyType Type => ProxyType.CSharp;

        [Required]
        [NotNull]
        public virtual string Url { get; set; }

        public virtual ServiceType ServiceType { get; set; }

        [CanBeNull]
        public virtual string Module { get; set; }

        public virtual bool WithoutContracts { get; set; }

        [CanBeNull]
        public virtual string Folder { get; set; }

        public AbpGenerateRemoveCSharpProxyInput()
        {
        }

        public AbpGenerateRemoveCSharpProxyInput([NotNull] string url, ServiceType serviceType,
            [CanBeNull] string module, bool withoutContracts, [CanBeNull] string folder)
        {
            Url = url;
            ServiceType = serviceType;
            Module = module;
            WithoutContracts = withoutContracts;
            Folder = folder;
        }
    }
}