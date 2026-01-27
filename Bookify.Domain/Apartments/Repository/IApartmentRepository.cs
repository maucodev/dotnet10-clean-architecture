using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartments.Repository;

public interface IApartmentRepository
{
    Task<Apartment> GetAsync(Guid id, CancellationToken cancellationToken = default);
}