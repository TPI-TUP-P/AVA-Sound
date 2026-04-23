using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IReproductionListService, ReproductionListService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
