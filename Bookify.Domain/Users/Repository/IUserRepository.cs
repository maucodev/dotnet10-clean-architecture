using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bookify.Domain.Users.Repository;

public interface IUserRepository
{
    void Add(User user);

    Task<User> GetAsync(Guid id, CancellationToken cancellationToken = default);
}