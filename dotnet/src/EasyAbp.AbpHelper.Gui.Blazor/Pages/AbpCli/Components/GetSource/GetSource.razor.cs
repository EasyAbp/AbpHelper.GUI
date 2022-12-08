﻿using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.GetSource;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.GetSource
{
    public partial class GetSource
    {
        [Inject]
        private IAbpCliGetSourceAppService Service { get; set; }

        public GetSource()
        {
            Input.Version = AbpVersionHelper.AbpVersion;
        }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GetSourceAsync(Input);
        }

        protected override void SetDirectoryToCurrentSolutionPath()
        {
            // Do nothing.
        }
    }
}