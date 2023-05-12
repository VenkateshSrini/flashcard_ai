using TechCards.lib.DBModel;

namespace TechCards.Repository
{
    public interface IFCDetailRepo
    {
        Task<bool> DeleteFCDetailsAsync(string fcDetailId);
        Task<List<FCDetailModel>> GetAllFCDetails();
        Task<FCDetailModel> GetFCDetailsByIdAsync(string fcDeatilId);
        Task<FCDetailModel> InsertFCDetailsAsync(FCDetailModel fCDetail);
        Task<bool> UpdateFCDetailsAsync(string fcDetailId, string resolution);
    }
}