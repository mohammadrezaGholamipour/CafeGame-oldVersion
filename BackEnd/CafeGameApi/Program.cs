using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CafeGameApi.ConfigModels;
using CafeGameApi.Context;
using CafeGameApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
var config = builder.Configuration;

//DataBase
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = System.IO.Path.Join(path, "CafeGame.db");

builder.Services
    .AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlite($"Data Source={dbPath}");
    });

//Identity
builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
        options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequiredLength = 4;
            options.Stores.ProtectPersonalData = false;
        })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//JWT
var jwtOptions = config.GetSection(nameof(JwtOptions)).Get<JwtOptions>() ??
                         throw new Exception("JwtOptions are not provided.");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
            };
            o.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.NoResult();
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new { title = "خطای احراز هویت", errors = new string[] { "وارد حساب نشده‌اید" } });
                    return context.Response.WriteAsync(result);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new
                    { title = "خطای دسترسی امنیتی", errors = new string[] { "شما به این قسمت دسترسی ندارید" } });
                    return context.Response.WriteAsync(result);
                }
            };
        });

builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new GlobalExceptionHandler());
    })
    .AddJsonOptions(
    opts =>
    {
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        opts.JsonSerializerOptions.AllowTrailingCommas = true;
    });
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors
builder.Services.AddCors(setup =>
{
    setup.AddPolicy("default", policy => policy
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (config.GetSection("swagger")?.Get<bool>() ?? false)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser<int>>>();

    if (!await roleManager.RoleExistsAsync
            (AppConstants.UserRoles.NotRegisteredUser))
    {
        var role = new IdentityRole<int>(AppConstants.UserRoles.NotRegisteredUser);
        var roleResult = await roleManager.CreateAsync(role);
    }

    if (!await roleManager.RoleExistsAsync
            (AppConstants.UserRoles.RegisteredUser))
    {
        var role = new IdentityRole<int>(AppConstants.UserRoles.RegisteredUser);
        var roleResult = await roleManager.CreateAsync(role);
    }

    if (!await roleManager.RoleExistsAsync
            (AppConstants.UserRoles.Admin))
    {
        var role = new IdentityRole<int>(AppConstants.UserRoles.Admin);
        var roleResult = await roleManager.CreateAsync(role);
    }

    if ((await userManager.FindByNameAsync("admin")) == null)
    {
        var user = await userManager.CreateAsync(new IdentityUser<int>("admin"), "admin123");
        var admin = await userManager.FindByNameAsync("admin");
        await userManager.AddToRoleAsync(admin!, AppConstants.UserRoles.Admin);
    }
}

app.UseCors("default");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:8505");
