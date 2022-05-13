using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos
{
    [Serializable]
    public class AbpGenerateRemoveJavaScriptProxyInput : InputDtoWithDirectory
    {
        public virtual ProxyType Type { get; set; }
        
        [Required]
        [NotNull]
        public virtual string Url { get; set; }
        
        [CanBeNull]
        public virtual string Module { get; set; }
        
        [CanBeNull]
        public virtual string Output { get; set; }

        public AbpGenerateRemoveJavaScriptProxyInput()
        {
        }

        public AbpGenerateRemoveJavaScriptProxyInput(ProxyType type, [NotNull] string url, [CanBeNull] string module,
            [CanBeNull] string output)
        {
            Type = type;
            Url = url;
            Module = module;
            Output = output;
        }
    }
}