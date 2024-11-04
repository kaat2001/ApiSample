using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.MsSql;

public static class DataAccessMsSqlModule
{
    public static IServiceCollection AddDataAccessMsSqlModule(
       this IServiceCollection services,
       IConfigurationManager configuration)
    {
        services.AddDbContextFactory<MsSqlDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString("MsSqlConnection"),
                builder => builder.MigrationsAssembly(typeof(MsSqlDbContext).Assembly.FullName)));

        return services;
    }
}
