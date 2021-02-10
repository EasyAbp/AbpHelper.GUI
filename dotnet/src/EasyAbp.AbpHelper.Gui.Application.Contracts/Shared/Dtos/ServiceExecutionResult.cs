namespace EasyAbp.AbpHelper.Gui.Shared.Dtos
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