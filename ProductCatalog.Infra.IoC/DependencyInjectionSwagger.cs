﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ProductCatalog.Infra.IoC;
public static class DependencyInjectionSwagger
{
    public static IServiceCollection AddInfrastuctureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Catalog", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " +
                "and then your token in the text input below. \r\n\r\nExample: \"Bearer 1234abcdef\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }
}
