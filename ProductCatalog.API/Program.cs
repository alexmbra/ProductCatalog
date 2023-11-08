using ProductCatalog.Infra.IoC;

namespace ProductCatalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddInfrastuctureAPI(builder.Configuration);
        builder.Services.AddInfrastuctureJWT(builder.Configuration);
        builder.Services.AddInfrastuctureSwagger();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStatusCodePages();
        app.UseAuthentication();
        app.UseAuthorization();
        

        app.MapControllers();

        app.Run();
    }
}
