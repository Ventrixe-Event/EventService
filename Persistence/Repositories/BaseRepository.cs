using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Models;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity>
    where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _table;

    public BaseRepository(DataContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.ToListAsync();
            return new RepositoryResult<IEnumerable<TEntity>> { Success = true, Result = entities };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<TEntity>>
            {
                Success = false,
                Error = ex.Message,
            };
        }
    }

    public async Task<RepositoryResult<TEntity?>> GetAsync(
        Expression<Func<TEntity, bool>> expression
    )
    {
        try
        {
            var entity = await _table.FirstOrDefaultAsync(expression);
            return new RepositoryResult<TEntity?>
            {
                Success = true,
                Result = entity ?? throw new Exception("Not found"),
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<TEntity?> { Success = false, Error = ex.Message };
        }
    }

    public async Task<RepositoryResult> AddAsync(TEntity entity)
    {
        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, Error = ex.Message };
        }
    }

    public async Task<RepositoryResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, Error = ex.Message };
        }
    }

    public async Task<RepositoryResult> DeleteAsync(TEntity entity)
    {
        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, Error = ex.Message };
        }
    }
}
