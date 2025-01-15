using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealTimeNotificationSys.Core.Interfaces;
using RealTimeNotificationSys.Core.Services;
using RealTimeNotificationSys.Infrastructure.Data;
using RealTimeNotificationSys.Infrastructure.Hubs;
using StackExchange.Redis;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IChannelService, ChannelService>();
builder.Services.AddScoped<INotificationService, NotificationService>();



// Add Redis connection for publishing and consuming messages
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    return ConnectionMultiplexer.Connect("localhost:6379");  // Update with your Redis server
});

// Register services for publishing and consuming notifications
builder.Services.AddScoped<NotificationPublisher>();
builder.Services.AddScoped<NotificationConsumer>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});




builder.Services.AddSignalR();


// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);




builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add authentication with JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"], // Corrected key
            ValidAudience = builder.Configuration["JwtSettings:Audience"], // Corrected key
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])) // Corrected key
        };
    });

builder.Services.AddControllers();

// Enabling Swagger for development (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// CORS configuration (optional, if you plan on connecting a frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
app.MapHub<NotificationHub>("/notificationHub");  // SignalR Hub URL for clients to connect
app.MapControllers();  // API Controllers

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseWebSockets();
// Use authentication middleware
app.UseAuthentication();

// Use authorization middleware
app.UseAuthorization();

// Enable CORS
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
