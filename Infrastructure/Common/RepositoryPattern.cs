using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Common
{
    public class RepositoryPattern<TEntity> : IRepositoryPattern<TEntity>
    where TEntity : class
    {
       

            protected readonly DbContext _context;
            protected readonly DbSet<TEntity> _dbSet;

            public RepositoryPattern(DbContext context)
            {
                _context = context;
                _dbSet = context.Set<TEntity>();
            }


            public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            {
            
                return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            }

            public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            {
                return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
            }
            public virtual async Task<IReadOnlyList<TEntity>> FindAsync(
                Expression<Func<TEntity, bool>> predicate,
                CancellationToken cancellationToken = default)
            {
                return await _dbSet.AsNoTracking()
                    .Where(predicate)
                    .ToListAsync(cancellationToken);
            }
            public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            {
                await _dbSet.AddAsync(entity, cancellationToken);
            }

            public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            {
                await _dbSet.AddRangeAsync(entities, cancellationToken);
            }

            public virtual void Update(TEntity entity)
            {
                _dbSet.Update(entity);
            }

            public virtual void Remove(TEntity entity)
            {
                _dbSet.Remove(entity);
            }

            public virtual void RemoveRange(IEnumerable<TEntity> entities)
            {
                _dbSet.RemoveRange(entities);
            }
        }
    }

