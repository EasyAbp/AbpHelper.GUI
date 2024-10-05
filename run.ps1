$folderPath = ".\dotnet\src\EasyAbp.AbpHelper.Gui.Blazor"
Start-Process "dotnet" -ArgumentList "watch run" -WorkingDirectory $folderPath -NoNewWindow -Wait