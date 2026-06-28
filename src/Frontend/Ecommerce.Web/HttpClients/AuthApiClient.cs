using Ecommerce.Web.ViewModels;
using System.Text.Json;

namespace Ecommerce.Web.HttpClients
{
    public class AuthApiClient : BaseApiClient
    {
        public AuthApiClient(HttpClient http, IHttpContextAccessor ctx) : base(http, ctx) { }

        public async Task<(bool Success, AuthResponseViewModel? Data, string? Error)> LoginAsync(LoginViewModel vm)
        {
            try
            {
                var res = await _http.PostAsync("/api/auth/login", Json(new { vm.Email, vm.Password }));
                var body = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode)
                    return (false, null, JsonSerializer.Deserialize<AuthApiError>(body, _json)?.Message ?? "Login failed");
                return (true, JsonSerializer.Deserialize<AuthResponseViewModel>(body, _json), null);
            }
            catch { return (false, null, "Unable to connect to auth service"); }
        }

        public async Task<(bool Success, string? Error)> RegisterAsync(RegisterViewModel vm)
        {
            try
            {
                var res = await _http.PostAsync("/api/auth/register", Json(new
                {
                    vm.FirstName,
                    vm.LastName,
                    vm.Email,
                    vm.Password,
                    vm.PhoneNumber
                }));
                if (!res.IsSuccessStatusCode)
                {
                    var body = await res.Content.ReadAsStringAsync();
                    return (false, JsonSerializer.Deserialize<AuthApiError>(body, _json)?.Message ?? "Registration failed");
                }
                return (true, null);
            }
            catch { return (false, "Unable to connect to auth service"); }
        }
    }

    // ── Local DTO – only used inside AuthApiClient ──
    internal record AuthApiError(string Message);
}
