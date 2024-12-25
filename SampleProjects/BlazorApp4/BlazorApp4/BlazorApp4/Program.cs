
using BlazorApp4.Components;
using BlazorApp4.Components.Account;
using BlazorApp4.Data;
using Class.Auth;
using Class.Controller;
using Class.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

//builder.Services.AddApiAuthorization();

builder.Services.AddHttpClient();

builder.Services.AddScoped<TableTypesService>();
builder.Services.AddScoped<FoodTypesService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<MenuResourceService>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<RoleMenuPermController>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ApiLoginService>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();
builder.Services.AddAuthorization(options =>
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
builder.Services.AddSingleton<IAuthorizationHandler, ResourceRoleAuthorizationHandler>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
//builder.Services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp4.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
