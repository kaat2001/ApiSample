using DataModel.DbContexts;
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
        services.AddScoped<IDbContext, MsSqlDbContext>();
        services.AddScoped<DbContext, MsSqlDbContext>();

        services.AddDbContextFactory<MsSqlDbContext>(
            options => options
            .UseSqlServer(
                configuration.GetConnectionString("MsSqlConnection"),
                builder => builder.MigrationsAssembly(typeof(MsSqlDbContext).Assembly.FullName))
            .UseSeeding((context, _) =>
            {
                SampleData.Sample.SeedData(context);
            }));


        return services;
    }
}
