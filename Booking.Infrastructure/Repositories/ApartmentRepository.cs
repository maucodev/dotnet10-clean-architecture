using Bookify.Domain.Apartments;
using Bookify.Domain.Apartments.Repository;

namespace Booking.Infrastructure.Repositories;

internal sealed class ApartmentRepository : Repository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}