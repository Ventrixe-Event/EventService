using System.Linq.Expressions;
using Persistence.Entities;
using Persistence.Models;

namespace Persistence.Interfaces;

public interface IEventRepository
{
    Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync();

    Task<RepositoryResult<EventEntity?>> GetAsync(Expression<Func<EventEntity, bool>> expression);

    Task<RepositoryResult> AddAsync(EventEntity entity);

    Task<RepositoryResult> UpdateAsync(EventEntity entity);

    Task<RepositoryResult> DeleteAsync(EventEntity entity);

    Task<RepositoryResult> AlreadyExistsAsync(Expression<Func<EventEntity, bool>> expression);
}
