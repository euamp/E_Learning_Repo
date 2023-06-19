using Blazored.LocalStorage;
using E_LearningWebApp.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;

namespace E_LearningWebApp.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorageService;
    private readonly IConfiguration _config;
    private string authTokenStoragekey;

    public AuthenticationService(HttpClient client,
                                 AuthenticationStateProvider authStateProvider,
                                 ILocalStorageService localStorageService,
                                 IConfiguration config)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        _localStorageService = localStorageService;
        _config = config;
        authTokenStoragekey = _config["authTokenStoragekey"];
    }

    public async Task<string> Login(LoginModel userForAuthentication)
    {
        string api = _config["tokenEndpoint"];

        var authResult = await _client.PostAsJsonAsync(api, userForAuthentication);

        var result = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode == false)
        {
            return null;
        }

        await _localStorageService.SetItemAsync(authTokenStoragekey, result);

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result);

        return result;
    }

    public async Task<string> Register(SignUpModel registerUserModel)
    {
        string api = _config["registrationEndpoint"];

        var authResult = await _client.PostAsJsonAsync(api, registerUserModel);

        var result = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode == false)
        {
            return null;
        }

        return result;
    }

    public async Task Logout()
    {
        await _localStorageService.RemoveItemAsync(authTokenStoragekey);
        // Casting it to the right type (To the class that i made that extends AuthenticationStateProvider)
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }
}
