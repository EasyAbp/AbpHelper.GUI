using System;
using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos
{
    [Serializable]
    public class ModuleDto
    {
        public string Id { get; set; }

        /// <summary>
        /// Overrides the ModuleGroupDto.Id if set.
        /// </summary>
        public string GroupId { get; set; }

        public string Submodule { get; set; }

        public List<string> DefaultTargets { get; set; }

        public bool Default { get; set; }

        #region Extra properties

        public bool Checked { get; set; }

        public bool Indeterminate { get; set; }

        public bool Installed { get; set; }

        public List<string> Targets { get; set; } = new();

        #endregion
    }
}