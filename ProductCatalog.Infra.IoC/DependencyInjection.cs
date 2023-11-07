using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Mappings;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Account;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Infra.Data.Context;
using ProductCatalog.Infra.Data.Identity;
using ProductCatalog.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductCatalog.Infra.IoC;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext)
                .Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/Account/Login";
        });

        services.AddScoped<IAuthenticate, AuthenticateService>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();


        services.AddScoped<IDapperRepository<Product>>(sp => new DapperProductRepository(connectionString));


        services.AddScoped<IRepository<Product>, ProductRepository>();
        services.AddScoped<IRepository<Category>, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
        services.AddAutoMapper(typeof(DTOToCommandMappingProfile));

        var myHandlers = AppDomain.CurrentDomain.Load("ProductCatalog.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(myHandlers));

        return services;
    }
}
