using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Web.Extensions;
using Core.Middlewares;
using Application.Interfaces.IJwtService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.RateLimiting;
using Infrastructure.Data.Services;
using System.Text;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Http.Headers;

// using System.Text;
// using Application.Interfaces;
// using Application.Interfaces.IJwtService;
// using Application.Services;
// using Core.Middlewares;
// using Domain.Interfaces;
// using Infrastructure.Data;
// using Infrastructure.Data.Repositories;
// using Infrastructure.Data.Services;
// using Infrastructure.Interfaces;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Http.Features;
// using Microsoft.Data.Sqlite;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi;
// using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// builder.Services.AddControllers();

builder.Services.AddControllers(options =>
{
    // saca el formatter que intenta parsear como JQuery
    var jqueryFormValueProviderFactory = options.ValueProviderFactories
        .OfType<JQueryFormValueProviderFactory>()
        .FirstOrDefault();

    if (jqueryFormValueProviderFactory != null)
        options.ValueProviderFactories.Remove(jqueryFormValueProviderFactory);
});




// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer(
        (document, context, cancellationToken) =>
        {
            var schemeName = "ApiBearerAuth";

            var securityScheme =
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Acá pegar el token"
                };

            document.Components ??= new OpenApiComponents();

            document.Components.SecuritySchemes ??=
                new Dictionary<string, IOpenApiSecurityScheme>();

            document.Components.SecuritySchemes[schemeName] =
                securityScheme;

            var schemeReference =
                new OpenApiSecuritySchemeReference(
                    schemeName,
                    document);

            var requirement =
                new OpenApiSecurityRequirement
                {
                    [schemeReference] = []
                };

            document.Security =
                new List<OpenApiSecurityRequirement>
                {
                    requirement
                };

            return Task.CompletedTask;
        });
});


    var connectionString = builder.Configuration["CONNECTION_STRING"] 
                        ?? builder.Configuration.GetConnectionString("DefaultConnection");
    var connection = new SqliteConnection(connectionString);
connection.Open();

using (var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode = DELETE ";
    command.ExecuteNonQuery();
}

builder.Services.AddDbContext<ApplicationContext>(DbContextOptions => DbContextOptions.UseSqlite(connection));

builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IAlbumService, AlbumService>();

builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();
builder.Services.AddScoped<IStatisticService, StatisticService>();

builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddScoped<IInfoUserService, InfoUserService>();
builder.Services.AddScoped<IInfoUserRepository, InfoUserRepository>();

builder.Services.AddScoped<IReproductionListService, ReproductionListService>();
builder.Services.AddScoped<IReproductionsListRepository, ReproductionsListRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<ISongService, SongService>();

builder.Services.AddCustomRateLimit(
    builder.Configuration);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddHttpClient<IStorageService, StorageService>(client=>
{
    var keyLogin = builder.Configuration["SUPABASE_KEY"] ?? builder.Configuration["Supabase:Key"];
    if(string.IsNullOrEmpty(keyLogin))
    {
        throw new Exception("The key is empty");
    }

    client.DefaultRequestHeaders.Add(
        "apikey",
        keyLogin
    );
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue(
            "Bearer",
            keyLogin
        );

});

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue; // permite archivos grandes
});

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(
        builder.Configuration["JWT_SECRET_KEY"] 
        ?? builder.Configuration["Jwt:Key"] 
        ?? throw new InvalidOperationException("JWT Key not found")))
        };
    });
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseDeveloperExceptionPage();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "My API V1");

        options.RoutePrefix = string.Empty;
    });

    app.MapOpenApi();
}

app.UseRateLimiter();




app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();




app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();




