using System.Globalization;
using ProductCatalog.Domain.Account;
using ProductCatalog.Infra.IoC;

namespace ProductCatalog.WebUI;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfrastucture(builder.Configuration);
        builder.Services.AddControllersWithViews();

        var cultureInfo = new CultureInfo("en-US"); // Replace "en-US" with the appropriate culture for your needs

        // Set the default culture for the application
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();

        customCulture.NumberFormat.NumberDecimalSeparator = ",";
        customCulture.NumberFormat.NumberGroupSeparator = ".";

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        builder.Services.AddMvc()
            .AddViewOptions(options =>
            {
                options.HtmlHelperOptions.FormInputRenderMode = Microsoft.AspNetCore.Mvc.Rendering.FormInputRenderMode.AlwaysUseCurrentCulture;
            });

        var app = builder.Build();



        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseRequestLocalization();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        SeedUserRoles(app).Wait();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static async Task SeedUserRoles(IApplicationBuilder app)
    {
        using IServiceScope? serviceScope = app.ApplicationServices.CreateScope();
        ISeedUserRoleInitial? seed = serviceScope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        if (seed is not null)
        {
            await seed.SeedRoles();
            await seed.SeedUsers();
        }
    }
}
