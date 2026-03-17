using CourtApp.Api.Extensions;
using CourtApp.Application.DTOs.Settings;
using CourtApp.Application.Extensions;
using CourtApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// ---------------- ENVIRONMENT ----------------
var environment = builder.Environment.EnvironmentName;
Console.WriteLine($"CourtApp.Api starting in {environment} environment");

// ---------------- LOGGING ----------------
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// ---------------- CONFIGURATION ----------------
builder.Services.Configure<JWTSettings>(
    builder.Configuration.GetSection("JWTSettings"));

// ---------------- LAYERS ----------------
builder.Services.AddApiServices();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);

// ---------------- CORS ----------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "https://yourdomain.com"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ---------------- CONTROLLERS ----------------
builder.Services.AddControllers();

// ---------------- BUILD APP ----------------
var app = builder.Build();

Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");


// ================= MIDDLEWARE PIPELINE =================

// Forward headers (for IIS / reverse proxy / nginx)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto
});

// Global exception handler
app.UseExceptionHandler("/error");

// Security headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    await next();
});

// HTTPS redirect
app.UseHttpsRedirection();

// Routing
app.UseRouting();

// CORS
app.UseCors("CorsPolicy");

// Authentication / Authorization
app.UseAuthentication();
app.UseAuthorization();

// Custom API middleware
app.UseApiMiddleware();


// ================= SWAGGER =================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CourtApp API v1");
        c.RoutePrefix = "swagger";
    });
}


// ================= ENDPOINTS =================

app.MapControllers();

Console.WriteLine("CourtApp.Api started successfully");

app.Run();
