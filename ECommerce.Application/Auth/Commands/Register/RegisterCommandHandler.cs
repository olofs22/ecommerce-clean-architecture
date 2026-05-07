using ECommerce.Application.Auth.DTOs;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using MediatR;

namespace ECommerce.Application.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterCommandHandler(IIdentityService identityService, IJwtTokenService jwtTokenService)
    {
        _identityService = identityService;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.RegisterAsync(request.Email, request.Password, request.Role);

        if (!result.Succeeded)
        {
            throw new UnauthorizedException(string.Join("; ", result.Errors));
        }

        var token = _jwtTokenService.GenerateToken(result.UserId!, result.Email, result.Roles);
        return new AuthResponseDto(token, result.Email, result.Roles);
    }
}