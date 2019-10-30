using DataAccess.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MongoRepository<TModel> where TModel : BaseModel
    {
        private readonly IMongoCollection<TModel> _repository;
        public MongoRepository(MongoDbConfiguration configuration)
        {
            _repository = new MongoContext<TModel>(configuration).Collection;
        }

        public async Task<ICollection<TModel>> GetAll()
        {
            return await _repository.Find(x=> true).ToListAsync();
        }

        public async Task Save(TModel model)
        {
            await _repository.InsertOneAsync(model);
        }

        public async Task<bool> Remove(TModel model)
        {
            var actionResult = await _repository.DeleteOneAsync(x=> x == model);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<bool> RemoveAll(Expression<Func<TModel, bool>> expression)
        {
            var actionResult = await _repository.DeleteManyAsync(expression);

            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<ICollection<TModel>> FindAll(Expression<Func<TModel, bool>> expression)
        {
            return await _repository.Find(expression).ToListAsync();
        }

        public async Task<ICollection<TModel>> GetPage(Guid id, int takeCount)
        {
            var filterBuilder = Builders<TModel>.Filter;
            var filter = filterBuilder.Gt(x => x.Id, id);
            return await _repository.Find(filter).Limit(takeCount).ToListAsync();
        }

        public IQueryable<TModel> GetAsQueryable()
        {
            return _repository.AsQueryable();
        }

    }
}
