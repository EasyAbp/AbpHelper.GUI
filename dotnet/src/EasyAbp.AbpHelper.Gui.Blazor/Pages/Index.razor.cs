﻿using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.UpdateCheck;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Volo.Abp.Cli;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        private IUpdateCheckAppService UpdateCheckAppService { get; set; }

        private bool UpdateCheckAlertVisible { get; set; }
        
        private string LatestVersion { get; set; }
        
        private string CurrentVersion { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var result = await UpdateCheckAppService.CheckAsync();

                LatestVersion = result.LatestVersion;
                CurrentVersion = result.CurrentVersion;
                UpdateCheckAlertVisible = result.ShouldUpdate;
            }
            catch (Exception e)
            {
                Logger.LogWarning(e, "Failed to check latest version");
                UpdateCheckAlertVisible = false;
            }
        }
    }
}