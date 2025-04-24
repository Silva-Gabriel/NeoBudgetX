using domain.interfaces.repository;
using domain.models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using services;

namespace app.auth
{
    public class AuthHandler : IRequestHandler<AuthRequest, AuthResponse>
    {
        private IUserWriteRepository _repository { get; }

        private ILogger<AuthHandler> _logger { get; }

        private IAuthService _authService { get; }

        private IConfiguration _configuration { get; }

        public AuthHandler(ILogger<AuthHandler> logger, IAuthService authService, IUserWriteRepository repository)
        {
            _logger = logger;
            _authService = authService;
            _repository = repository;
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();
        }

        public async Task<AuthResponse> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
            var response = new AuthResponse();

            var expiration = _configuration.GetValue<int>("jwtToken:expirationMinutes");
            var expirationDateTime = DateTime.UtcNow.AddMinutes(expiration);
            var authentication = new Authentication
            {
                User = request.User,
                Password = request.Password
            };
            
            var token = _authService.GenerateToken(authentication, _configuration.GetValue<string>("jwtToken:key"), expirationDateTime);
            var passwordHash = await _repository.GetPasswordHash(authentication, cancellationToken);


            if (passwordHash == null || passwordHash == string.Empty)
            {
                _logger.LogWarning("Usuário ou senha inválidos!", request.User);
                return response;
            }
            
            var authValidation = BCrypt.Net.BCrypt.Verify(request.Password, passwordHash);


            if (authValidation)
            {
                _logger.LogInformation("Usuário autenticado com sucesso!");
                response = new AuthResponse { Token = token };

                return response;
            }

            return response;
        }
    }
}