using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Update.Dtos
{
    [Serializable]
    public class AbpUpdateInput
    {
        [Required]
        [NotNull]
        public virtual string RunningPath { get; set; }
        
        [CanBeNull]
        public virtual string SolutionPath { get; set; }
        
        [CanBeNull]
        public virtual string SolutionName { get; set; }
        
        [CanBeNull]
        public virtual string Version { get; set; }
        
        public virtual bool CheckAll { get; set; }

        public virtual bool Npm { get; set; }
        
        public virtual bool Nuget { get; set; }
        
        public AbpUpdateInput()
        {
        }

        public AbpUpdateInput([NotNull] string runningPath, [CanBeNull] string solutionPath,
            [CanBeNull] string solutionName, [CanBeNull] string version, bool checkAll, bool npm, bool nuget)
        {
            RunningPath = runningPath;
            SolutionPath = solutionPath;
            SolutionName = solutionName;
            Version = version;
            CheckAll = checkAll;
            Npm = npm;
            Nuget = nuget;
        }
    }
}