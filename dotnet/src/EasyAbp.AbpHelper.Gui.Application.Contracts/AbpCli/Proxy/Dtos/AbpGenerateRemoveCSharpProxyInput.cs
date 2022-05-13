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
        
        [CanBeNull]
        public virtual string Module { get; set; }

        [CanBeNull]
        public virtual string Folder { get; set; }

        public AbpGenerateRemoveCSharpProxyInput()
        {
        }

        public AbpGenerateRemoveCSharpProxyInput([NotNull] string url, [CanBeNull] string module,
            [CanBeNull] string folder)
        {
            Url = url;
            Module = module;
            Folder = folder;
        }
    }
}