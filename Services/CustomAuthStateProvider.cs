using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Mini_Project_Kredit.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _session;

        private ClaimsPrincipal _currentUser =
            new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ProtectedSessionStorage session)
        {
            _session = session;
        }

        // ✅ Safe during prerender: NO JS interop here
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
            => Task.FromResult(new AuthenticationState(_currentUser));

        // ✅ Call this ONLY after interactive (e.g., OnAfterRenderAsync)
        public async Task RefreshAsync()
        {
            try
            {
                var stored = await _session.GetAsync<string>("token");
                var token = stored.Success ? stored.Value : null;

                if (string.IsNullOrWhiteSpace(token))
                {
                    _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
                }
                else
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, "User")
                    }, authenticationType: "SessionToken");

                    _currentUser = new ClaimsPrincipal(identity);
                }

                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
            catch
            {
                // If JS interop still isn't ready, keep anonymous
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }

        public async Task SignOutAsync()
        {
            try { await _session.DeleteAsync("token"); } catch { /* ignore */ }
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
