using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IMongoAsyncRepository<TModel> where TModel : BaseModel
    {
        Task<ICollection<TModel>> FindAll();
        Task<ICollection<TModel>> FindAll(Expression<Func<TModel, bool>> expression);
        Task<ICollection<TModel>> GetAll();
        IQueryable<TModel> GetAsQueryable();
        Task<TModel> GetById(Guid id);
        Task<ICollection<TModel>> GetPage(Guid id, int takeCount);
        Task<bool> Remove(TModel model);
        Task<bool> RemoveAll();
        Task<bool> RemoveAll(Expression<Func<TModel, bool>> expression);
        Task Save(TModel model);
        Task Upsert(TModel model);
    }
}