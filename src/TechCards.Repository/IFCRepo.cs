using TechCards.lib.DBModel;

namespace TechCards.Repository
{
    public interface IFCRepo
    {
        Task<bool> DeleteFCDetailsAsync(string fcId);
        Task<List<FCModel>> GetAllFCDetails();
        Task<FCModel> GetFCDetailsByIdAsync(string fcId);
        Task<FCModel> InsertFCDetailsAsync(FCModel fCDetail);
        Task<bool> UpdateFCDetailsAsync(string fcId, string cardText);
    }
}