using Bookify.Domain.Apartments;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bookify.Domain.Bookings.Repository;

public interface IBookingRepository
{
    void Add(Booking booking);

    Task<Booking> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(
        Apartment apartment,
        DateRange duration,
        CancellationToken cancellationToken = default);
}