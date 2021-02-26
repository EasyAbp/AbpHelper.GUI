using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Update.Dtos
{
    [Serializable]
    public class AbpUpdateInput : InputDtoWithDirectory
    {
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

        public AbpUpdateInput([NotNull] string directory, [CanBeNull] string solutionPath,
            [CanBeNull] string solutionName, [CanBeNull] string version, bool checkAll, bool npm, bool nuget) : base(
            directory)
        {
            SolutionPath = solutionPath;
            SolutionName = solutionName;
            Version = version;
            CheckAll = checkAll;
            Npm = npm;
            Nuget = nuget;
        }
    }
}