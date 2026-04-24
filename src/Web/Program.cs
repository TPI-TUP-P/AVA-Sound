using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IReproductionListService, ReproductionListService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var connection = new SqliteConnection("Data Source=AVA_Sound.db" );
connection.Open();

using(var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode = DELETE ";
    command.ExecuteNonQuery();
}
builder.Services.AddDbContext<ApplicationContext>(DbContextOptions => DbContextOptions.UseSqlite(connection));
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
});


}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
