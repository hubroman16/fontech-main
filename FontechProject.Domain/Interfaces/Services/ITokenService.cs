using System.Security.Claims;
using FontechProject.Domain.Dto;
using FontechProject.Domain.Result;

namespace FontechProject.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();

    ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string accessToken);

    Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
}