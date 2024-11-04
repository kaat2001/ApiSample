using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.PgSql;

public static class DataAccessPgSqlModule
{
    public static IServiceCollection AddDataAccessMsSqlModule(
       this IServiceCollection services,
       IConfiguration configuration)
    {

        services.AddDbContextFactory<PgSqlDbContext>(
            options => options.UseNpgsql(
                configuration.GetConnectionString("PgSqlConnection"),
                builder => builder.MigrationsAssembly(typeof(PgSqlDbContext).Assembly.FullName)));


        return services;
    }
}
