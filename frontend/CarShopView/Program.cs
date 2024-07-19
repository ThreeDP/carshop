using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CarShopView;
using Refit;
using CarShopView.Repositories;
using CarShopView.Models;
using CarShopView.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorBootstrap();
builder.Services.AddHttpClient();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddScoped<ICustomer, Customer>();
builder.Services.AddScoped<IQueryCustomers, QueryCustomers>();
builder.Services.AddScoped<IPaginationHeader, PaginationHeader>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUser, User>();
builder.Services.AddSingleton<IAuthService, AuthService>();
//var baseAddress = builder.Configuration["BaseAddress"] ?? throw new ArgumentException("Error: back end api not set.");
builder.Services.AddScoped(sp =>
    new HttpClient {
        BaseAddress = new Uri("http://localhost:9000")
});
builder.Services.AddRefitClient<ICustomerRepository>().ConfigureHttpClient(c => {
    c.BaseAddress = new Uri("http://localhost:9000");
});
builder.Services.AddRefitClient<IUserRepository>().ConfigureHttpClient(c => {
    c.BaseAddress = new Uri("http://localhost:9000");
});
builder.Services.AddRefitClient<IVehicleRepository>().ConfigureHttpClient(c => {
    c.BaseAddress = new Uri("http://localhost:9000");
});
var app = builder.Build();
await app.RunAsync();
