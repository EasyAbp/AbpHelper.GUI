using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Login.Dtos
{
    [Serializable]
    public class AbpLoginInput
    {
        [Required]
        [NotNull]
        public virtual string Username { get; set; }
        
        [Required]
        [NotNull]
        public virtual string Password { get; set; }
        
        [CanBeNull]
        public virtual string Organization { get; set; }

        public AbpLoginInput()
        {
        }

        public AbpLoginInput([NotNull] string username, [NotNull] string password, [CanBeNull] string organization)
        {
            Username = username;
            Password = password;
            Organization = organization;
        }
    }
}