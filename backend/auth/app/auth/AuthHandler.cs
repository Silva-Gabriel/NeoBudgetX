using MediatR;
using Microsoft.Extensions.Logging;

namespace app.auth
{
    public class AuthHandler : IRequestHandler<AuthRequest, AuthResponse>
    {
        private ILogger _logger { get; }

        public AuthHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
            var response = new AuthResponse { IsValid = false };

            if (request.User == "123" && BCrypt.Net.BCrypt.Verify(request.User, "$2a$12$cZKmnSLSGc1xwge3rBj13OZg6y.cvrIrOzz6d67zEsEQG3FBac9YS"))
            {
                _logger.LogInformation("Usuário autenticado com sucesso!");
                response = new AuthResponse { IsValid = true };

                return response;
            }

            return response;
        }
    }
}