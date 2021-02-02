namespace EasyAbp.AbpHelper.Gui.Services.Shared.Dtos
{
    public class ServiceExecutionResult
    {
        public virtual bool Success { get; set; }

        public ServiceExecutionResult(bool success)
        {
            Success = success;
        }
    }
}