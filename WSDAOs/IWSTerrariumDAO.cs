using Model;

namespace WSDAOs;

public interface IWSTerrariumDAO
{
    public Task<TerrariumLimits> GetTerrariumLimitsAsync();
}