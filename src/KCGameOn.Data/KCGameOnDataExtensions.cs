using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KCGameOn.Data;

public static class KCGameOnDataExtensions
{
    public static IServiceCollection AddKCGameOnData(this IServiceCollection services, string databaseConnectionString)
    {
        return services.AddDbContext<KCGameOnContext>(options => options.UseMySQL(databaseConnectionString), ServiceLifetime.Singleton);
    }
}
