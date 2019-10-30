using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IMongoRepository<TModel> where TModel : BaseModel
    {
        ICollection<TModel> FindAll();
        ICollection<TModel> FindAll(Expression<Func<TModel, bool>> expression);
        ICollection<TModel> GetAll();
        IQueryable<TModel> GetAsQueryable();
        TModel GetById(Guid id);
        ICollection<TModel> GetPage(Guid id, int takeCount);
        bool Remove(TModel model);
        bool RemoveAll();
        bool RemoveAll(Expression<Func<TModel, bool>> expression);
        void Save(TModel model);
        void Upsert(TModel model);
    }
}