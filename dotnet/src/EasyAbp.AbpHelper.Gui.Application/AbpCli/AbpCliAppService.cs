using System;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared;
using Volo.Abp.Application.Services;
using Volo.Abp.Cli.Args;

namespace EasyAbp.AbpHelper.Gui.AbpCli
{
    public abstract class AbpCliAppService : ApplicationService
    {
        public static CommandLineArgs CreateCommandLineArgs(object input, string command = null, string target = null)
        {
            var args = new CommandLineArgs(command, target);

            if (input == null)
            {
                return args;
            }

            foreach (var propertyInfo in input.GetType().GetProperties())
            {
                var optionKey = propertyInfo.Name.PascalToKebabCase();
                
                if (propertyInfo.PropertyType == typeof(string))
                {
                    var value = (string) propertyInfo.GetValue(input);

                    if (value != null)
                    {
                        args.Options.Add(optionKey, value);
                    }
                }
                else if (propertyInfo.PropertyType == typeof(bool))
                {
                    if ((bool) propertyInfo.GetValue(input))
                    {
                        args.Options.Add(optionKey, null);
                    }
                }
                else if (typeof(Enum).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    args.Options.Add(optionKey,
                        Attribute.IsDefined(propertyInfo.PropertyType, typeof(ToStringUseDescriptionAttribute))
                            ? (propertyInfo.GetValue(input) as Enum)?.ToDescriptionString()
                            : propertyInfo.GetValue(input)?.ToString().PascalToKebabCase());
                }
            }

            return args;
        }
    }
}