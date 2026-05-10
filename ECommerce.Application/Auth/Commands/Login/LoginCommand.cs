using ECommerce.Application.Auth.DTOs;
using MediatR;

namespace ECommerce.Application.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;