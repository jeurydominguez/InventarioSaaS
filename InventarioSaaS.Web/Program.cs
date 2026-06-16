using ApexCharts;
using Blazored.LocalStorage;
using InventarioSaaS.Web;
using InventarioSaaS.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddScoped<ToastService>();
builder.Services.AddApexCharts();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtAuthHandler>();
builder.Services.AddScoped<SalesService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AccountsReceivableService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<NotificationStateService>();
builder.Services.AddScoped<SettingsService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<JwtAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("admin",
        policy => policy.RequireClaim("rol", "admin"));
});
builder.Services.AddHttpClient(
    "Api",
    client =>
    {
        client.BaseAddress =
            new Uri("https://localhost:7210/");
    })
    .AddHttpMessageHandler<JwtAuthHandler>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
