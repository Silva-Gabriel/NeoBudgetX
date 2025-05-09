using System.Data;
using Dapper;
using domain.interfaces.repository;
using domain.models;

namespace infra.repository
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private IDbConnection _connection { get; }

        public UserWriteRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> GetPasswordHash(Authentication auth, CancellationToken cancellationToken)
        {
            var query = $@"SELECT PASSWORD_HASH 
                            FROM TB_USERS
                            WHERE USERNAME = @username";

            var passwordHash = await _connection.QueryFirstOrDefaultAsync<string>(query, new { username =  auth.User });

            return passwordHash ?? string.Empty;
        }

        public Task<int> GetRole(string user)
        {
            var query = $@"SELECT ACCESS_GROUP
                            FROM TB_USERS
                            WHERE USERNAME = @username";

            var role = _connection.QueryFirstOrDefaultAsync<int>(query, new { username = user });
            return role;
        }
    }
}