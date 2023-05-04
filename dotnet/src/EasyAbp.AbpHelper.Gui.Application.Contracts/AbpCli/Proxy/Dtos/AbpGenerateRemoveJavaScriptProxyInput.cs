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

        [CanBeNull]
        public virtual string Module { get; set; }

        [CanBeNull]
        public virtual string Output { get; set; }

        public AbpGenerateRemoveJavaScriptProxyInput()
        {
        }

        public AbpGenerateRemoveJavaScriptProxyInput([NotNull] string url, [CanBeNull] string module,
            [CanBeNull] string output)
        {
            Url = url;
            Module = module;
            Output = output;
        }
    }
}