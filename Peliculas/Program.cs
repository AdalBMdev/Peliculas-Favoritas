using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Peliculas.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<DB_peliculasContext>(builder.Configuration.GetConnectionString("Conex"));

// Add session services.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Use session middleware.
app.UseSession();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(a => a.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthorization();

// Add this before mapping controllers.
app.UseSession();

app.MapControllers();

app.Run();


