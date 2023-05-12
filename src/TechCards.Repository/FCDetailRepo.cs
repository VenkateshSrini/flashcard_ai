using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCards.lib.DBModel;

namespace TechCards.Repository
{
    public class FCDetailRepo : IFCDetailRepo
    {
        private IMongoCollection<FCDetailModel> _fcDetailCollection;
        private readonly ILogger<FCDetailRepo> _logger;
        public FCDetailRepo(IMongoClient mongoClient, MongoUrl mongoUrl,
            ILogger<FCDetailRepo> logger)
        {
            _logger = logger;
            var db = mongoClient.GetDatabase(mongoUrl?.DatabaseName);
            var collectionName = "FCDetails";
            _fcDetailCollection = db.GetCollection<FCDetailModel>(collectionName);
        }
        public async Task<FCDetailModel> InsertFCDetailsAsync(FCDetailModel fCDetail)
        {
            await _fcDetailCollection.InsertOneAsync(fCDetail);
            return fCDetail;
        }
        public async Task<FCDetailModel> GetFCDetailsByIdAsync(string fcDeatilId)
        {
            return (await _fcDetailCollection.FindAsync(saga => saga.Id == fcDeatilId)).FirstOrDefault();
        }
        public async Task<List<FCDetailModel>> GetAllFCDetails()
        {
            return await _fcDetailCollection.AsQueryable().ToListAsync();
        }
        public async Task<bool> UpdateFCDetailsAsync(string fcDetailId, string resolution)
        {
            var fcDetail = new FCDetailModel();
            var filter = Builders<FCDetailModel>.Filter.Eq(nameof(fcDetail.Id), fcDetailId);
            var update = Builders<FCDetailModel>.Update.Set(nameof(fcDetail.Resolution), resolution);
            var modifiedCount = await _fcDetailCollection.UpdateOneAsync(filter, update);

            return modifiedCount.ModifiedCount > 0;
        }
        public async Task<bool> DeleteFCDetailsAsync(string fcDetailId)
        {
            var fcDetail = new FCDetailModel();
            var filter = Builders<FCDetailModel>.Filter.Eq(nameof(fcDetail.Id), fcDetailId);
            var deletedResult = await _fcDetailCollection.DeleteOneAsync(filter);
            return deletedResult.DeletedCount > 0;
        }
    }
}
