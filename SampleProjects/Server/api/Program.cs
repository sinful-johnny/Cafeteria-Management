using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Interfaces;
using Microsoft.Extensions.Options;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EFTut.Repository;
using api.Service;
using api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Controller
builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


//DbContext services
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//Identity services
builder.Services.AddIdentity<AppUser, IdentityRole>(options => //extend the roles 
                                                               //extend the AppUser 
{
    options.Password.RequireDigit = true; //Require numbers within the password

    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

    options.Password.RequireNonAlphanumeric = true;

    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDBContext>(); //Entity Framework Stores

//Add Schema
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var signingKeyEncryption = builder.Configuration["JWT:SigningKey"];

    if (signingKeyEncryption == null)
        signingKeyEncryption = "";

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(signingKeyEncryption))//Encryption
    };
});


//Hook Interfaces and Repository in
builder.Services.AddScoped<ICanvaRepository, CANVA_repository>();
builder.Services.AddScoped<IAdminRepository, ADMIN_repository>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
