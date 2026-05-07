namespace ECommerce.Application.Auth.DTOs;

public record AuthResponseDto(string Token, string Email, IList<string> Roles);