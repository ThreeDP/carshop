using CarShop.Filters;
using CarShop.Repositories;
using CarShop.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class CarShopServiceCollectionExtensions {
    public static IServiceCollection AddContextConfig(
        this IServiceCollection services, IConfiguration config) {
        return services;
    }

    /* Configura as injeções de dependências dos repositores. */
    public static IServiceCollection AddRepositoriesDependencyGroup(
        this IServiceCollection services) {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        return services;
    }

    /* Configura as injeções de dependências relacionas a serviços */
    public static IServiceCollection AddServicesDependencyGroup(
        this IServiceCollection services) {
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }

    /* Configura as injeções de dependências relacionas a filtros */
    public static IServiceCollection AddFiltersDependencyGroup(
        this IServiceCollection services) {
        services.AddScoped<CarShopLoggingFilter>();
        return services;
    }
}