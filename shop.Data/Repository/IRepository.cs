using shop.Core.Commons;
using shop.Data.ApplicationContext;
using System.Linq.Expressions;

namespace shop.Data.Repository
{
    public partial interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        TEntity FindById(params object[] ids);
        Task<TEntity?> FindByIdAsync(params object[] ids);
        TEntity FindByIdAsNoTracking(params object[] ids);
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableAsNoTracking { get; }
        List<T> RunSp<T>(string StoreName, List<DbParamter> ListParamert) where T : new();
        Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> filter);
    }
}
