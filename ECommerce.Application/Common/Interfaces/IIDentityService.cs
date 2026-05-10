namespace ECommerce.Application.Common.Interfaces;

public record AuthResult(bool Succeeded, string? UserId, string Email, IList<string> Roles, string[] Errors);

public interface IIdentityService
{
    Task<AuthResult> RegisterAsync(string email, string password, string role);
    Task<AuthResult> LoginAsync(string email, string password);
}