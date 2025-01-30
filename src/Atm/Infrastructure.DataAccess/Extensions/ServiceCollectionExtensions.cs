using Application.Abstractions.Repositories;
using Infrastructure.DataAccess.Repositories;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection services,
        Action<PostgresConnectionConfiguration> configurePostgres)
    {
        services.AddPlatformPostgres(builder => builder.Configure(configurePostgres));
        services.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}