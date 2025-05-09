using domain.interfaces.repository;
using domain.models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace app.createRegister
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        private IUserWriteRepository _repository { get; }
    
        private ILogger<RegisterHandler> _logger { get; }

        private IConfiguration _configuration { get; }

        public RegisterHandler(IUserWriteRepository repository, ILogger<RegisterHandler> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _repository = repository;
        }

        public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var responseAuth = new Authentication 
            {
                User = request.User,
                Password = request.Password,
                Role = request.Role
            };

            _logger.LogInformation($"Registering user {request.User}");

            var salt = int.Parse(_configuration["bcrypt:salt"]);
            responseAuth.Password = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
            var result = await _repository.CreateUser(responseAuth, cancellationToken);

            return new RegisterResponse { Id = result };
        }
    }
}