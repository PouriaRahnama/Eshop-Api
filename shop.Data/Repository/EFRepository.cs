using Microsoft.EntityFrameworkCore;
using shop.Core.Commons;
using shop.Data.ApplicationContext;
using System.Linq.Expressions;

namespace shop.Data.Repository
{
    public partial class EFRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

        #region Filed
        private readonly IApplicationContext _context;
        public EFRepository(IApplicationContext context)
        {
            _context = context;
        }

        private DbSet<TEntity> entities;
        protected  DbSet<TEntity> Entities
        {
            get
            {
                if (entities == null)
                    entities = _context.Set<TEntity>();
                else
                {
                    return entities;
                }

                return entities;
            }
        }

        #endregion

        public virtual IQueryable<TEntity> Table => Entities;

        public virtual IQueryable<TEntity> TableAsNoTracking => Entities.AsNoTracking();

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }

            return queryable.ToList();
        }

        public virtual async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> filter)
        {
           return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity?> GetEntityNoTracking(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity?> FindByIdAsync(params object[] ids)
        {
            return await _context.Set<TEntity>().FindAsync(ids);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual List<T> RunSp<T>(string StoreName, List<DbParamter> ListParamert) where T : new()
        {
            return _context.RunSp<T>(StoreName, ListParamert);
        }


    }
}
