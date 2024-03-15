using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Templates.Dtos;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Templates
{
    public class RecentlyTemplatesManager : IRecentlyTemplatesManager, ITransientDependency
    {
        private static readonly string PersonalDirectoryPath =
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        private static readonly string SettingsDirectoryPath = Path.Combine(PersonalDirectoryPath, ".abphelper-gui");

        private static readonly string FilePath = Path.Combine(SettingsDirectoryPath, "RecentlyTemplates.json");

        public async Task<List<TemplateDto>> GetListAsync()
        {
            if (!File.Exists(FilePath))
            {
                Directory.CreateDirectory(SettingsDirectoryPath);

                await File.WriteAllTextAsync(FilePath, JsonConvert.SerializeObject(new List<TemplateDto>()));
            }

            var result = JsonConvert.DeserializeObject<List<TemplateDto>>(await File.ReadAllTextAsync(FilePath));

            if (!result.Any(x => x.DisplayName == "Default"))
            {
                result.Add(new TemplateDto
                {
                    DisplayName = "Default",
                    DirectoryPath = string.Empty
                });
            }

            return result;
        }

        public async Task UpdateListAsync(List<TemplateDto> input)
        {
            input ??= new List<TemplateDto>();

            await File.WriteAllTextAsync(FilePath, JsonConvert.SerializeObject(input));
        }
    }
}