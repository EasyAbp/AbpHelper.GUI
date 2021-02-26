using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Add.Dtos
{
    [Serializable]
    public class AbpAddModuleInput : InputDtoWithDirectory
    {
        [Required]
        [NotNull]
        public virtual string ModuleName { get; set; }
        
        [CanBeNull]
        public virtual string Solution { get; set; }
        
        [CanBeNull]
        public virtual string StartupProject { get; set; }
        
        public virtual bool SkipDbMigrations { get; set; }

        public virtual bool New { get; set; }
        
        public virtual bool WithSourceCode { get; set; }
        
        public virtual bool AddToSolutionFile { get; set; }

        public AbpAddModuleInput()
        {
        }

        public AbpAddModuleInput([NotNull] string directory, [NotNull] string moduleName, [CanBeNull] string solution,
            [CanBeNull] string startupProject, bool skipDbMigrations, bool @new, bool withSourceCode,
            bool addToSolutionFile) : base(directory)
        {
            ModuleName = moduleName;
            Solution = solution;
            StartupProject = startupProject;
            SkipDbMigrations = skipDbMigrations;
            New = @new;
            WithSourceCode = withSourceCode;
            AddToSolutionFile = addToSolutionFile;
        }
    }
}