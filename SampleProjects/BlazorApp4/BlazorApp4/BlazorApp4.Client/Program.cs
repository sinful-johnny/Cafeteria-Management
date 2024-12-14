using Class.Auth;
using Class.Controller;
using Class.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore(
    options =>
    {
        options.AddPolicy("ReadWrite", policy =>
        {
            policy.Requirements.Add(new ResourceRoleRequirement("ReadWrite"));
        });
        options.AddPolicy("Read", policy =>
        {
            policy.Requirements.Add(new ResourceRoleRequirement("Read"));
        });
    });
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddHttpClient();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<TableTypesService>();
builder.Services.AddScoped<FoodTypesService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<MenuResourceService>();
builder.Services.AddSingleton<IAuthorizationHandler, ResourceRoleAuthorizationHandler>();


await builder.Build().RunAsync();
