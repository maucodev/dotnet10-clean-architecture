using Bookify.Domain.Users;
using Bookify.Domain.Users.Repository;

namespace Booking.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}