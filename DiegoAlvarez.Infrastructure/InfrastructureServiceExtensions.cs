using DiegoAlvarez.Application.Interfaces.Security;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using DiegoAlvarez.Infrastructure.Context;
using DiegoAlvarez.Infrastructure.Implementations.Security;
using DiegoAlvarez.Infrastructure.Implementations.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoAlvarez.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
