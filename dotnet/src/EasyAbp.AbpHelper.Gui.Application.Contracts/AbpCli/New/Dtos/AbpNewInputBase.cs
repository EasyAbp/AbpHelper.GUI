﻿using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    [Serializable]
    public abstract class AbpNewInputBase
    {
        [NotNull]
        public abstract string Template { get; }

        [Required]
        [NotNull]
        public virtual string SolutionName { get; set; }

        [Required]
        [NotNull]
        public virtual string OutputFolder { get; set; }

        [CanBeNull]
        public virtual string Version { get; set; }

        public virtual bool Preview { get; set; }

        [CanBeNull]
        public virtual string TemplateSource { get; set; }

        public virtual bool CreateSolutionFolder { get; set; }

        [CanBeNull]
        public virtual string ConnectionString { get; set; }

        public virtual Database DatabaseManagementSystem { get; set; }

        [CanBeNull]
        public virtual string LocalFrameworkRef { get; set; }

        public virtual bool NoRandomPort { get; set; }

        public virtual bool SkipInstallingLibs { get; set; }

        public virtual bool SkipCache { get; set; }

        public virtual bool WithPublicWebsite { get; set; }

        public AbpNewInputBase()
        {
        }

        public AbpNewInputBase([NotNull] string solutionName, [NotNull] string outputFolder, [CanBeNull] string version,
            bool preview, [CanBeNull] string templateSource, bool createSolutionFolder,
            [CanBeNull] string connectionString, Database databaseManagementSystem,
            [CanBeNull] string localFrameworkRef, bool noRandomPort, bool skipInstallingLibs, bool skipCache,
            bool withPublicWebsite)
        {
            SolutionName = solutionName;
            OutputFolder = outputFolder;
            Version = version;
            Preview = preview;
            TemplateSource = templateSource;
            CreateSolutionFolder = createSolutionFolder;
            ConnectionString = connectionString;
            DatabaseManagementSystem = databaseManagementSystem;
            LocalFrameworkRef = localFrameworkRef;
            NoRandomPort = noRandomPort;
            SkipInstallingLibs = skipInstallingLibs;
            SkipCache = skipCache;
            WithPublicWebsite = withPublicWebsite;
        }
    }
}