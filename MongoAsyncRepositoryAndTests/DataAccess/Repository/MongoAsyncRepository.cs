using DataAccess.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MongoAsyncRepository<TModel> : IMongoAsyncRepository<TModel> where TModel : BaseModel
    {
        private readonly IMongoCollection<TModel> _repository;
        public MongoAsyncRepository(MongoDbConfiguration configuration)
        {
            _repository = new MongoContext<TModel>(configuration).Collection;
        }

        public async Task<TModel> GetById(Guid id)
        {
            return await _repository.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<TModel>> GetAll()
        {
            return await _repository.Find(x => true).ToListAsync();
        }

        public async Task Save(TModel model)
        {
            await _repository.InsertOneAsync(model);
        }

        public async Task Upsert(TModel model)
        {
            var updateOptions = new UpdateOptions()
            {
                IsUpsert = true
            };

            await _repository.ReplaceOneAsync(x => x.Id == model.Id, model, options: updateOptions);
        }

        public async Task<bool> Remove(TModel model)
        {
            var actionResult = await _repository.DeleteOneAsync(x => x == model);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<bool> RemoveAll()
        {
            var actionResult = await _repository.DeleteManyAsync(x => true);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<bool> RemoveAll(Expression<Func<TModel, bool>> expression)
        {
            var actionResult = await _repository.DeleteManyAsync(expression);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<ICollection<TModel>> FindAll()
        {
            return await _repository.Find(x => true).ToListAsync();
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
