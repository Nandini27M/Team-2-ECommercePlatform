using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Web.HttpClients
{
    public abstract class BaseApiClient
    {
        protected readonly HttpClient _http;
        protected readonly IHttpContextAccessor _ctx;
        protected static readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

        protected BaseApiClient(HttpClient http, IHttpContextAccessor ctx)
        {
            _http = http;
            _ctx = ctx;
        }

        protected void AttachToken()
        {
            var token = _ctx.HttpContext?.Session.GetString("jwt_token");
            if (!string.IsNullOrEmpty(token))
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected StringContent Json<T>(T payload) =>
            new(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
    }
}
