using TechCards.lib.DBModel;

namespace TechCards.Services
{
    public interface IChatGPTServices
    {
        Task<bool> FindResolution(FCDetailModel fCDetail);
    }
}