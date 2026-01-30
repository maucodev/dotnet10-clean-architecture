using Bookify.Domain.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

internal abstract class Repository<T> where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public void Add(T entity)
    {
        DbContext.Add(entity);
    }
}