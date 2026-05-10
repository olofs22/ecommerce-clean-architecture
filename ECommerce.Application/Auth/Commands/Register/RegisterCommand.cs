using ECommerce.Application.Auth.DTOs;
using MediatR;

namespace ECommerce.Application.Auth.Commands.Register;

public record RegisterCommand(string Email, string Password, string Role) : IRequest<AuthResponseDto>;