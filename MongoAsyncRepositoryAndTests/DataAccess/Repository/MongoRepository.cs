using DataAccess.Interfaces;
using DataAccess.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace DataAccess.Repository
{
    public class MongoRepository<TModel> : IMongoRepository<TModel> where TModel : BaseModel
    {
        private readonly IMongoCollection<TModel> _repository;
        public MongoRepository(MongoDbConfiguration configuration)
        {
            _repository = new MongoContext<TModel>(configuration).Collection;
        }

        public TModel GetById(Guid id)
        {
            return _repository.Find(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<TModel> GetAll()
        {
            return _repository.Find(x => true).ToList();
        }

        public void Save(TModel model)
        {
            _repository.InsertOne(model);
        }

        public void Upsert(TModel model)
        {
            var updateOptions = new UpdateOptions()
            {
                IsUpsert = true
            };

            _repository.ReplaceOne(x => x.Id == model.Id, model, options: updateOptions);
        }

        public bool Remove(TModel model)
        {
            var actionResult = _repository.DeleteOne(x => x == model);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public bool RemoveAll()
        {
            var actionResult = _repository.DeleteMany(x => true);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public bool RemoveAll(Expression<Func<TModel, bool>> expression)
        {
            var actionResult = _repository.DeleteMany(expression);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public ICollection<TModel> FindAll()
        {
            return _repository.Find(x => true).ToList();
        }

        public ICollection<TModel> FindAll(Expression<Func<TModel, bool>> expression)
        {
            return _repository.Find(expression).ToList();
        }

        public ICollection<TModel> GetPage(Guid id, int takeCount)
        {
            var filterBuilder = Builders<TModel>.Filter;
            var filter = filterBuilder.Gt(x => x.Id, id);
            return _repository.Find(filter).Limit(takeCount).ToList();
        }

        public IQueryable<TModel> GetAsQueryable()
        {
            return _repository.AsQueryable();
        }

    }
}
