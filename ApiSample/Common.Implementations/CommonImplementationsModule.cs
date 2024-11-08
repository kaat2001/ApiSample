using Microsoft.Extensions.DependencyInjection;

namespace Common.Implementations;

public static class CommonImplementationsModule
{
    public static IServiceCollection AddCommonImplementationsModule(
       this IServiceCollection services)

    {
        services.AddAutoMapper(typeof(AppMappingProfile));
        return services;
    }
}