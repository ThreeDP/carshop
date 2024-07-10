
using CarShop.Context;
using CarShop.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using CarShop.Filters;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Logger;
using CarShop.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarShop.Models;
using CarShop.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => {
    options.Filters.Add(typeof(ApiExceptionFilter));
})
.AddJsonOptions(opts =>
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarShop API", Version = "v1"});
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT ",
    });
    c.AddSecurityRequirement( new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services
    .AddCors();

/* Configurações de Injeção de dependência declaradas em extensions */
builder.Services
    .AddRepositoriesDependencyGroup()
    .AddServicesDependencyGroup()
    .AddFiltersDependencyGroup();

builder.Services.AddDbContext<CarShopDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("defaultConn")));

builder.Logging.AddProvider(new CustomLoggerProvider( new CustomLoggerProviderConfig {
    LogLevel = LogLevel.Information
}));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<CarShopDataContext>()
        .AddDefaultTokenProviders();

var secretKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalid secret key!!");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:validIsuser"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey)
        )
    };
});
builder.Services.AddAuthorization();
builder.Configuration.AddEnvironmentVariables();
var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    app.ConfigureExceptionHandler();
    app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true));
                    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    // .AllowCredentials());
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
