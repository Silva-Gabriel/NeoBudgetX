using domain.models;

namespace services
{
    public interface IAuthService
    {
        public string GenerateToken(Authentication authRequest, string secretKey, DateTime expiration);
    }
}