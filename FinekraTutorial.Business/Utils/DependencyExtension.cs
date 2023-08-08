using FinekraTutorial.Business.Interfaces;
using FinekraTutorial.Business.Services;
using FinekraTutorial.DataAccess.Context;
using FinekraTutorial.DataAccess.Interfaces.Repositores;
using FinekraTutorial.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinekraTutorial.Business.Utils;

public static class DependencyExtension
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
        services.AddScoped(typeof(IOrderService), typeof(OrderService<FinekraDbContext>));
    }
}
