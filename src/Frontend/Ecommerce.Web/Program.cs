using Ecommerce.Web.HttpClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt => {
    opt.IdleTimeout = TimeSpan.FromMinutes(60);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

// Register HTTP Clients (point to your API Gateway URL)
var gateway = builder.Configuration["GatewayUrl"] ?? "https://localhost:7288";

builder.Services.AddHttpClient<AuthApiClient>
    (c => c.BaseAddress = new Uri(gateway));
builder.Services.AddHttpClient<ProductApiClient>
    (c => c.BaseAddress = new Uri(gateway));
builder.Services.AddHttpClient<CartApiClient>
    (c => c.BaseAddress = new Uri(gateway));
builder.Services.AddHttpClient<OrderApiClient>
    (c => c.BaseAddress = new Uri(gateway));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapRazorPages();

app.MapGet("/", () => Results.Redirect("/Auth/Login"));

app.Run();
