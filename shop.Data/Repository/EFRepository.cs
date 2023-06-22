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

        protected DbSet<TEntity> Entities
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

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }

            return queryable.ToList();
        }

        public async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> filter)
        {
           return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity?> FindByIdAsync(params object[] ids)
        {
            return await _context.Set<TEntity>().FindAsync(ids);
        }

        public TEntity FindByIdAsNoTracking(params object[] ids)
        {
            var entity = _context.Set<TEntity>().Find(ids);
            if (entity != null)
            {
                //NoTracking
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }


        public virtual IQueryable<TEntity> Table => Entities;


        public virtual IQueryable<TEntity> TableAsNoTracking => Entities.AsNoTracking();


        public List<T> RunSp<T>(string StoreName, List<DbParamter> ListParamert) where T : new()
        {
            return _context.RunSp<T>(StoreName, ListParamert);
        }


        public virtual void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }



        public async Task AddAsync(TEntity entity)
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


        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }


        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.Deleted = true;
            _context.SaveChanges();
        }



        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.Deleted = true;
            await UpdateAsync(entity);
            await _context.SaveChangesAsync();
        }


        public virtual TEntity FindById(params object[] ids)
        {
            return _context.Set<TEntity>().Find(ids);
        }


    }
}
