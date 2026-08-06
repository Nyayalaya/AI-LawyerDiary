using CourtApp.Api.Extensions;
using CourtApp.Application.DTOs.Settings;
using CourtApp.Application.Extensions;
using CourtApp.Infrastructure.AI.Extensions;
using CourtApp.Infrastructure.DataSeeder;
using CourtApp.Infrastructure.Extensions;
using CourtApp.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Text;

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

var jwtSettings = builder.Configuration
    .GetSection("JWTSettings")
    .Get<JWTSettings>();

// ---------------- LAYERS ----------------
builder.Services.AddApiServices();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAIServices(builder.Configuration);


// ---------------- CORS ----------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ---------------- 🔥 DISABLE COOKIE REDIRECT ----------------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

// ---------------- ✅ AUTHENTICATION (FIXED) ----------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // dev only
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key)
        ),

        ClockSkew = TimeSpan.Zero
    };

    // 🔥 VERY IMPORTANT → prevent redirect
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("❌ JWT Failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("✅ JWT Validated");
            return Task.CompletedTask;
        }
    };
});

// ---------------- CONTROLLERS ----------------
builder.Services.AddControllers();

// ---------------- BUILD APP ----------------
var app = builder.Build();

// -------------------- SEED DATA --------------------
using (var scope = app.Services.CreateScope())
{  
    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
    await AppMasterSeeder.SeedAsync(scope.ServiceProvider);
}

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

// ✅ MUST BE IN THIS ORDER
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
