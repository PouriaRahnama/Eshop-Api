using shop.Core.Commons;
using shop.Data.ApplicationContext;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace shop.Data.Repository
{
    public partial interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> filter);
        Task<TEntity?> GetEntityNoTracking(Expression<Func<TEntity, bool>> filter);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity?> FindByIdAsync(params object[] ids);
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableAsNoTracking { get; }
        List<T> RunSp<T>(string StoreName, List<DbParamter> ListParamert) where T : new();    
    }
}
