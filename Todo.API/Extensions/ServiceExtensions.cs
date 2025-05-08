using Microsoft.EntityFrameworkCore;
using Todo.Core.IRepository;
using Todo.Infrastructure.ApplicationContext;
using Todo.Infrastructure.Repository;
using Todo.Service.AutoMapperConfiguration;
using Todo.Service.IService;
using Todo.Service.Service;

namespace Todo.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
            options.EnableSensitiveDataLogging();
        });
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager , RepositoryManager>();
    }

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager , ServiceManager>();
    }

    public static void ConfigureAutoMapperService(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(TodoMappingProfile));
    }

    public static void ConfigureCORS(this IServiceCollection service)
    {
        service.AddCors(options =>
        {
            options.AddPolicy("AllowCORS" ,
                policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });
    }

}
