using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer
{
    public class ModuleLibrarySourcesManager : IModuleLibrarySourcesManager, ISingletonDependency
    {
        private static readonly string PersonalDirectoryPath =
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        private static readonly string SettingsDirectoryPath = Path.Combine(PersonalDirectoryPath, ".abphelper-gui");
        
        private static readonly string FilePath = Path.Combine(SettingsDirectoryPath, "ModuleLibrarySources.json");

        private static readonly List<ModuleLibrarySourceDto> DefaultSources = new()
        {
            new ModuleLibrarySourceDto
            {
                Name = "Default",
                IndexUrl = "https://raw.githubusercontent.com/EasyAbp/ModuleLibrary/v1/index.json"
            }
        };

        public async Task<List<ModuleLibrarySourceDto>> GetListAsync()
        {
            if (!File.Exists(FilePath))
            {
                Directory.CreateDirectory(SettingsDirectoryPath);
                
                await File.WriteAllTextAsync(FilePath, JsonConvert.SerializeObject(DefaultSources));
            }
            
            return JsonConvert.DeserializeObject<List<ModuleLibrarySourceDto>>(await File.ReadAllTextAsync(FilePath));
        }

        public async Task UpdateListAsync(List<ModuleLibrarySourceDto> input)
        {
            input ??= new List<ModuleLibrarySourceDto>();

            await File.WriteAllTextAsync(FilePath, JsonConvert.SerializeObject(input));
        }
    }
}