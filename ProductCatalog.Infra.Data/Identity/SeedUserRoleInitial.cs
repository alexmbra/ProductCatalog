using ProductCatalog.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace ProductCatalog.Infra.Data.Identity;
public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedUsers()
    {
        var newUser = await _userManager.FindByEmailAsync("alexmbra@hotmail.com");
        if (newUser is null)
        {
            newUser = new()
            {
                UserName = "alexmbra@gmail.com",
                Email = "alexmbra@gmail.com",
                NormalizedUserName = "ALEXMBRA@GMAIL.COM",
                NormalizedEmail = "ALEXMBRA@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(newUser, "Numsey#2023").Result;
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
            }
        }

        var newUser2 = await _userManager.FindByEmailAsync("Carlos.l.frazao@gmail.com");
        if (newUser2 is null)
        {
            newUser2 = new()
            {
                UserName = "alexmbra@hotmail.com",
                Email = "alexmbra@hotmail.com",
                NormalizedUserName = "ALEXMBRA@HOTMAIL.COM",
                NormalizedEmail = "ALEXMBRA@HOTMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(newUser2, "Numsey#2023").Result;
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser2, "Admin");
            }
        }
    }

    public async Task SeedRoles()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            await _roleManager.CreateAsync(role);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            IdentityRole role = new()
            {
                Name = "User",
                NormalizedName = "USER"
            };
            await _roleManager.CreateAsync(role);
        }
    }
}
