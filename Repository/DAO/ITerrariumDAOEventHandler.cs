using Model;

namespace Repository.DAO;

public interface ITerrariumDAOEventHandler
{
    void PublishTerrariumLimitCreated(TerrariumLimits terrariumLimits);
}