using System.Text;
using BikeShop.Application;
using BikeShop.Domain.Entities;
using BikeShop.Infrastructure;
using BikeShop.Infrastructure.Identity;
using BikeShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
//Настройка swagger для работы с JWT 
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Example: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//// Add Identity services
//builder.Services
//    .AddIdentity<ApplicationUser, IdentityRole<int>>()
//    .AddEntityFrameworkStores<BikeShopDbContext>()
//    .AddDefaultTokenProviders();
builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        // Пока можно оставить настройки по умолчанию.
        // Позже сюда перенесём требования к паролю.
    })
    .AddRoles<IdentityRole<int>>() //И в будущем у нас почти наверняка появятся роли: Customer и Admin
    .AddEntityFrameworkStores<BikeShopDbContext>()
    .AddDefaultTokenProviders();


//Именно этот middleware будет:
//читать заголовок. Если пришёл заголовок
//Authorization: Bearer xxxxxxx, 
//то:
//проверить подпись;
//проверить срок действия;
//проверить Issuer;
//проверить Audience.
//создавать HttpContext.User
//builder.Services
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,

//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],

//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
//        };
//    });
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                //Console.WriteLine("JWT: OnMessageReceived");
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                //Console.WriteLine($"JWT ERROR: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                //Console.WriteLine("JWT: Token validated");
                return Task.CompletedTask;
            }
        };
    });

//регистрирует сервисы авторизации. Именно благодаря ей работают [Authorize] и <AuthorizeView> в контроллерах
builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<BikeShopDbContext>();

    await db.Database.MigrateAsync();
    await DbInitializer.InitializeAsync(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//порядок критически важен:
app.UseAuthentication(); //  читает JWT, создаёт HttpContext.User
app.UseAuthorization(); //  смотрит: есть ли [Authorize]?

app.MapControllers();

app.Run();
