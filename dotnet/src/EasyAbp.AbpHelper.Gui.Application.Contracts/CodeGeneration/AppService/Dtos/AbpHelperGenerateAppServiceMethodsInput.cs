using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.AppService.Dtos
{
    [Serializable]
    public class AbpHelperGenerateAppServiceMethodsInput : AbpHelperGenerateInput
    {
        [Required]
        [NotNull]
        public virtual string MethodNames { get; set; }

        [Required]
        [NotNull]
        public virtual string ServiceName { get; set; }

        public virtual bool NoInput { get; set; }

        public virtual bool NoOutput { get; set; }

        public virtual bool IntegrationService { get; set; }

        public AbpHelperGenerateAppServiceMethodsInput()
        {
        }

        public AbpHelperGenerateAppServiceMethodsInput([NotNull] string directory, [CanBeNull] string projectName,
            [CanBeNull] string exclude, bool noOverwrite, [NotNull] string methodNames, [NotNull] string serviceName,
            bool noInput, bool noOutput) : base(directory, projectName, exclude, noOverwrite)
        {
            MethodNames = methodNames;
            ServiceName = serviceName;
            NoInput = noInput;
            NoOutput = noOutput;
        }
    }
}