using Model;

namespace WSDAOs;

public interface IWSBoundariesDAO
{
    public Task<TerrariumBoundaries> GetBoundariesAsync();
}