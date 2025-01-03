using Common.Implementation.Services;
using Common.Implementations.Repositories;
using Common.Interfaces.Repositories;
using Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Implementations;

public static class CommonImplementationsModule
{
    public static IServiceCollection AddCommonImplementationsModule(
       this IServiceCollection services)

    {
        services.AddAutoMapper(typeof(AppMappingProfile));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}