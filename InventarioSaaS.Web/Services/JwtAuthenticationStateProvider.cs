using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace InventarioSaaS.Web.Services;

public class JwtAuthenticationStateProvider
    : AuthenticationStateProvider
{
    private readonly ILocalStorageService localStorage;

    public JwtAuthenticationStateProvider(
        ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    public override async Task<AuthenticationState>
        GetAuthenticationStateAsync()
    {
        var token =
            await localStorage.GetItemAsync<string>(
                "authToken");

        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity()));
        }

        var claims = ParseClaims(token);

        var identity =
            new ClaimsIdentity(
                claims,
                "jwt");

        var user =
            new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication(
        string token)
    {
        var claims = ParseClaims(token);

        var identity =
            new ClaimsIdentity(
                claims,
                "jwt");

        var user =
            new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        var anonymous =
            new ClaimsPrincipal(
                new ClaimsIdentity());

        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(
                    anonymous)));
    }

    private IEnumerable<Claim> ParseClaims(
        string jwt)
    {
        var handler =
            new JwtSecurityTokenHandler();

        var token =
            handler.ReadJwtToken(jwt);

        return token.Claims;
    }
}