using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorAuth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private Task<AuthenticationState> _authenticationStateTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return _authenticationStateTask;
        }

        public Task SetAuthenticationState(Task<AuthenticationState> authenticationStateTask)
        {
            _authenticationStateTask = authenticationStateTask;
            NotifyAuthenticationStateChanged(_authenticationStateTask);
            return Task.CompletedTask;
        }
    }
}