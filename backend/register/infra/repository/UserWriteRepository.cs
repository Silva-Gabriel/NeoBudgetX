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

        public async Task<long> CreateUser(Authentication auth, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("username", auth.User, DbType.String);
            parameters.Add("password_hash", auth.Password, DbType.String);
            parameters.Add("role", (int)auth.Role, DbType.String);
            parameters.Add("created_at", DateTime.Now, DbType.DateTime);
            parameters.Add("active", true, DbType.Boolean);

            var query = @"
                INSERT INTO TB_USERS (USERNAME, PASSWORD_HASH, ACTIVE, CREATED_AT, ACCESS_GROUP)
                OUTPUT INSERTED.id
                VALUES (@username, @password_hash, @active, @created_at, @role);
            ";

            var result = await _connection.QuerySingleAsync<long>(query, parameters);
            return result;
        }
    }
}