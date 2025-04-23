using MediatR;

namespace app.auth
{
    public class AuthRequest : IRequest<AuthResponse>
    {
        public string User { get; set; }

        public string Password { get; set; }
    }
}