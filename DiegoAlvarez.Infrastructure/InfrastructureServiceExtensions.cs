using DiegoAlvarez.Application.Interfaces.Security;
using DiegoAlvarez.Application.Interfaces.UnitOfWork;
using DiegoAlvarez.Application.UseCases.Auth;
using DiegoAlvarez.Application.UseCases.Responses;
using DiegoAlvarez.Application.UseCases.Tickets;
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

        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUseCase>();

        services.AddScoped<GetAllTicketsUseCase>();
        services.AddScoped<GetTicketByIdUseCase>();
        services.AddScoped<CreateTicketUseCase>();
        services.AddScoped<UpdateTicketStatusUseCase>();

        services.AddScoped<GetResponsesByTicketUseCase>();
        services.AddScoped<CreateResponseUseCase>();

        return services;
    }
}
