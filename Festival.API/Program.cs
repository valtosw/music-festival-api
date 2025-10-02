using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MusicFestival.API.DTOs;
using MusicFestival.Core.Services;
using MusicFestival.Core.Services.Abstractions;
using MusicFestival.Data;
using MusicFestival.Data.Datasets;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

var auth0Domain = configuration["Auth0:Domain"];
var auth0Audience = configuration["Auth0:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = $"https://{auth0Domain}/";
    options.Audience = auth0Audience;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name"
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireClaim("permissions", "admin"));

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(MappingProfile).Assembly);
});

builder.Services.AddHttpClient<ITelegramService, TelegramService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Festival API", Version = "v1" });
    var authority = $"https://{auth0Domain}/";
    var oauthScheme = new OpenApiSecurityScheme
    {
        Name = "Auth0",
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(authority + "authorize"),
                TokenUrl = new Uri(authority + "oauth/token"),
                Scopes = new Dictionary<string, string>
                {
                { "openid", "OpenID Connect scope" },
                { "profile", "Profile scope" },
                { "email", "Email scope" },
                { "read:festivals", "Read festivals" },
                { "write:festivals", "Write festivals" }
                }
            }
        }
    };

    c.AddSecurityDefinition("oauth2", oauthScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [oauthScheme] = ["openid", "profile", "email", "read:festivals", "write:festivals"]
    });
});

builder.Services.AddScoped<DataSeeder>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Festival API v1");
    c.RoutePrefix = "swagger"; // open at /swagger


    // Configure OAuth for Swagger UI
    c.OAuthClientId(configuration["Auth0:ClientId"]);
    c.OAuthClientSecret(configuration["Auth0:ClientSecret"]);
    c.OAuthUsePkce();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();
}

await app.RunAsync();
