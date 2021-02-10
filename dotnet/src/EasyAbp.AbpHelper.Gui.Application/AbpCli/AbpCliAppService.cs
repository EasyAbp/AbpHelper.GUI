using System;
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

            foreach (var propertyInfo in input.GetType().GetProperties())
            {
                var optionKey = propertyInfo.Name.PascalToKebabCase();
                
                if (propertyInfo.PropertyType == typeof(string))
                {
                    args.Options.Add(optionKey, (string) propertyInfo.GetValue(input));
                }
                else if (propertyInfo.PropertyType == typeof(bool))
                {
                    var value = (bool?) propertyInfo.GetValue(input);
                    args.Options.Add(optionKey, value.ToString());
                }
                else if (typeof(Enum).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    args.Options.Add(optionKey, propertyInfo.GetValue(input)?.ToString().ToLower());
                }
            }

            return args;
        }
    }
}