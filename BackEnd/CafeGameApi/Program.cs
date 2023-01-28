using System.Text.Json.Serialization;
using CafeGameApi.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers()
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
builder.Services.AddCors(setup =>
{
    setup.AddPolicy("default", policy => policy
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});
builder.Services.AddApplicationInsightsTelemetry();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Configuration.GetSection("swagger")?.Get<bool>() ?? false)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.UseCors("default");

app.MapControllers();

app.Run("http://localhost:8505");
