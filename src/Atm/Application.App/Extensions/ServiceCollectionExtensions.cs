using Application.App.Transactions;
using Application.App.Users;
using Application.Contracts.Transactions;
using Application.Contracts.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application.App.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<CurrentUserService>();
        services.AddScoped<ICurrentUserService>(
            p => p.GetRequiredService<CurrentUserService>());

        return services;
    }
}