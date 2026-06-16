using System.Net.Http.Json;
using Blazored.LocalStorage;
using InventarioSaaS.Web.Models.Auth;

namespace InventarioSaaS.Web.Services;

public class AuthService
{
    private readonly IHttpClientFactory _factory;

    private readonly ILocalStorageService _localStorage;
    private readonly JwtAuthenticationStateProvider
    _authProvider;

    public AuthService(
        IHttpClientFactory factory,
        ILocalStorageService localStorage,
        JwtAuthenticationStateProvider authProvider)
    {
        _factory = factory;
        _localStorage = localStorage;
        this._authProvider = authProvider;
    }

    public async Task<bool> Login(
        LoginRequest request)
    {
        var client = _factory.CreateClient("Api");

        var response =
            await client.PostAsJsonAsync(
                "api/auth/login",
                request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
            

        var result =
            await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result is null)
            return false;

        await _localStorage.SetItemAsync(
            "authToken",
            result.Token);

        _authProvider.NotifyUserAuthentication(
    result.Token);

        return true;
    }

    public async Task<string?> GetToken()
    {
        return await _localStorage.GetItemAsync<string>(
            "authToken");
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(
            "authToken");
        _authProvider.NotifyUserLogout();
    }
    public async Task<UsuarioActualDto?> ObtenerUsuarioActual()
    {
        var client = _factory.CreateClient("Api");

        return await client.GetFromJsonAsync<UsuarioActualDto>(
            "api/auth/me");
    }
    public async Task ReenviarConfirmacion(
    string email)
    {
        var client =
            _factory.CreateClient("Api");

        var response =
            await client.PostAsync(
                $"api/auth/resend-confirmation?email={email}",
                null);

        response.EnsureSuccessStatusCode();
    }
}