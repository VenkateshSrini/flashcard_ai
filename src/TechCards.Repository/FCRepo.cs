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
    public class FCRepo : IFCRepo
    {
        private IMongoCollection<FCModel> _fcCollection;
        private readonly ILogger<FCRepo> _logger;
        public FCRepo(IMongoClient mongoClient, MongoUrl mongoUrl,
            ILogger<FCRepo> logger)
        {
            _logger = logger;
            var db = mongoClient.GetDatabase(mongoUrl?.DatabaseName);
            var collectionName = "FCDetails";
            _fcCollection = db.GetCollection<FCModel>(collectionName);
        }
        public async Task<FCModel> InsertFCDetailsAsync(FCModel fCDetail)
        {
            await _fcCollection.InsertOneAsync(fCDetail);
            return fCDetail;
        }
        public async Task<FCModel> GetFCDetailsByIdAsync(string fcId)
        {
            return (await _fcCollection.FindAsync(fc => fc.Id == fcId)).FirstOrDefault();
        }
        public async Task<List<FCModel>> GetAllFCDetails()
        {
            return await _fcCollection.AsQueryable().ToListAsync();
        }
        public async Task<bool> UpdateFCDetailsAsync(string fcId, string cardText)
        {
            var flashCard = new FCModel();
            var filter = Builders<FCModel>.Filter.Eq(nameof(flashCard.Id), fcId);
            var update = Builders<FCModel>.Update.Set(nameof(flashCard.CardDisplayText), cardText);
            var modifiedCount = await _fcCollection.UpdateOneAsync(filter, update);

            return modifiedCount.ModifiedCount > 0;
        }
        public async Task<bool> DeleteFCDetailsAsync(string fcId)
        {
            var flashCard = new FCModel();
            var filter = Builders<FCModel>.Filter.Eq(nameof(flashCard.Id), fcId);
            var deletedResult = await _fcCollection.DeleteOneAsync(filter);
            return deletedResult.DeletedCount > 0;
        }
    }
}
