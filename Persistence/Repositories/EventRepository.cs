using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Models;

namespace Persistence.Repositories;

public class EventRepository(DataContext context) : BaseRepository<EventEntity>(context)
{
    public override async Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.Include(x => x.Packages).ToListAsync();
            return new RepositoryResult<IEnumerable<EventEntity>>
            {
                Success = true,
                Result = entities,
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<EventEntity>>
            {
                Success = false,
                Error = ex.Message,
            };
        }
    }
}
