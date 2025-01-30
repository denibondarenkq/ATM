using Itmo.Dev.Platform.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess.Extensions;

public static class ServiceScopeExtensions
{
    public static void UseInfrastructureDataAccess(this IServiceScope scope)
    {
        scope.UsePlatformMigrationsAsync(default).GetAwaiter().GetResult();
    }
}