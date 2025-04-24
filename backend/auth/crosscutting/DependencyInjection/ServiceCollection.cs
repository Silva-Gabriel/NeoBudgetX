using System.Data;
using System.Reflection;
using System.Text;
using domain.interfaces.repository;
using infra.repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using services;

namespace crosscutting.DependencyInjection
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.Load("app");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("jwtToken:key"))),
                    };
                });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserWriteRepository, UserWriteRepository>();
            services.AddLogging();

            var connectionString = configuration.GetSection("dbConfig")["ConnectionString"];
            services.AddScoped<IDbConnection>(provider => new SqlConnection(connectionString));
            services.AddScoped(provider =>
            {
                var connection = provider.GetRequiredService<IDbConnection>();
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                return connection.BeginTransaction();
            });

            return services;
        }
    }
}