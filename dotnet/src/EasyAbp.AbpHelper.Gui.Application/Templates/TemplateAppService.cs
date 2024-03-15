using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Core.Services;
using EasyAbp.AbpHelper.Gui.Templates.Dtos;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.Templates
{
    public class TemplateAppService : ApplicationService, ITemplateAppService
    {
        private const byte RecentlyTemplatesMaxCount = 10;

        private readonly IRecentlyTemplatesManager _manager;

        public TemplateAppService(
            IRecentlyTemplatesManager manager)
        {
            _manager = manager;
        }

        public virtual async Task<ListResultDto<TemplateDto>> GetListAsync()
        {
            return new(await _manager.GetListAsync());
        }

        public virtual async Task<TemplateDto> UseAsync(TemplateDto input)
        {
            Check.NotNullOrWhiteSpace(input.DisplayName, nameof(input.DisplayName));

            var list = await _manager.GetListAsync();

            var template = FindTemplate(list, input);

            if (template == null)
            {
                template = input;

                list.AddFirst(template);
            }
            else
            {
                list.MoveItem((x) => x == template, 0);
            }

            if (!await IsTemplateDirectoryValidAsync(template))
            {
                list.RemoveAt(0);

                await UpdateRecentlyTemplateListAsync(list);

                throw new BusinessException("Gui:InvalidTemplateDirectoryPath");
            }

            await UpdateRecentlyTemplateListAsync(list);

            return input;
        }

        protected virtual async Task UpdateRecentlyTemplateListAsync(List<TemplateDto> list)
        {
            if (list.Count > RecentlyTemplatesMaxCount)
            {
                list = list.GetRange(0, RecentlyTemplatesMaxCount);
            }

            await _manager.UpdateListAsync(list);
        }

        protected virtual Task<bool> IsTemplateDirectoryValidAsync(TemplateDto template)
        {
            return Task.FromResult(string.IsNullOrWhiteSpace(template.DirectoryPath) || Directory.Exists(template.DirectoryPath));
        }

        protected virtual TemplateDto FindTemplate(IEnumerable<TemplateDto> templates, TemplateDto target)
        {
            return templates.FirstOrDefault(x => x.DirectoryPath == target.DirectoryPath);
        }

        public virtual async Task DeleteAsync(TemplateDto input)
        {
            var list = await _manager.GetListAsync();

            list.Remove(FindTemplate(list, input));

            await UpdateRecentlyTemplateListAsync(list);
        }
    }
}