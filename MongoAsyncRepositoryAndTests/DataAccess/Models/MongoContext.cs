using MongoDB.Driver;

namespace DataAccess.Models
{
    public class MongoContext<TModel> where TModel: BaseModel
    {
        readonly IMongoDatabase _repository;
        public MongoContext(MongoDbConfiguration config)
        {
            var client = new MongoClient(config.ConnectionString);
            _repository = client.GetDatabase(config.Database);
        }

        public IMongoCollection<TModel> Collection
        {
            get
            {
                return _repository.GetCollection<TModel>(typeof(TModel).Name);
            }
        }
    }
}
