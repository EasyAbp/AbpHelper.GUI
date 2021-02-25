using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Login;
using EasyAbp.AbpHelper.Gui.AbpCli.Login.Dtos;
using EasyAbp.AbpHelper.Gui.Common;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Cli.Commands;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Login
{
    public class AbpCliLoginAppService : AbpCliAppService, IAbpCliLoginAppService
    {
        private readonly LoginCommand _loginCommand;
        private readonly LogoutCommand _logoutCommand;
        private readonly ICurrentDirectoryHelper _currentDirectoryHelper;

        public AbpCliLoginAppService(
            LoginCommand loginCommand,
            LogoutCommand logoutCommand,
            ICurrentDirectoryHelper currentDirectoryHelper)
        {
            _loginCommand = loginCommand;
            _logoutCommand = logoutCommand;
            _currentDirectoryHelper = currentDirectoryHelper;
        }

        public virtual async Task<ServiceExecutionResult> LoginAsync(AbpLoginInput input)
        {
            var args = CreateCommandLineArgs(input, "abp login", input.Username);

            await _loginCommand.ExecuteAsync(args);

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> LogoutAsync()
        {
            var args = CreateCommandLineArgs(null, "abp logout");

            await _logoutCommand.ExecuteAsync(args);

            return new ServiceExecutionResult(true);
        }
    }
}