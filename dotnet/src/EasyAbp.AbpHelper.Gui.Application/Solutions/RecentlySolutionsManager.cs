using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Solutions
{
    public class RecentlySolutionsManager : IRecentlySolutionsManager, ITransientDependency
    {
        private static readonly string PersonalDirectoryPath =
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        private static readonly string SettingsDirectoryPath = Path.Combine(PersonalDirectoryPath, ".abphelper-gui");
        
        private static readonly string FilePath = Path.Combine(SettingsDirectoryPath, "RecentlySolutions.json");

        public async Task<List<SolutionDto>> GetListAsync()
        {
            if (!File.Exists(FilePath))
            {
                Directory.CreateDirectory(SettingsDirectoryPath);
                
                await File.WriteAllTextAsync(FilePath, JsonConvert.SerializeObject(new List<SolutionDto>()));
            }
            
            return JsonConvert.DeserializeObject<List<SolutionDto>>(await File.ReadAllTextAsync(FilePath));
        }

        public async Task UpdateListAsync(List<SolutionDto> input)
        {
            input ??= new List<SolutionDto>();

            await File.WriteAllTextAsync(FilePath, JsonConvert.SerializeObject(input));
        }
    }
}