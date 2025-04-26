using domain.models;

namespace domain.interfaces.repository
{
    public interface IUserWriteRepository
    {
        Task<long> CreateUser(Authentication auth, CancellationToken cancellationToken);
    }
}