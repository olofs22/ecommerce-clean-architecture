using ECommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<AuthResult> RegisterAsync(string email, string password, string role)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true  // simplification for learning project
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return new AuthResult(false, null, email, new List<string>(),
                result.Errors.Select(e => e.Description).ToArray());
        }

        // Make sure the role exists, then assign.
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
        }
        await _userManager.AddToRoleAsync(user, role);

        var roles = await _userManager.GetRolesAsync(user);
        return new AuthResult(true, user.Id, email, roles, Array.Empty<string>());
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return new AuthResult(false, null, email, new List<string>(), new[] { "Invalid credentials." });
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordValid)
        {
            return new AuthResult(false, null, email, new List<string>(), new[] { "Invalid credentials." });
        }

        var roles = await _userManager.GetRolesAsync(user);
        return new AuthResult(true, user.Id, email, roles, Array.Empty<string>());
    }
}