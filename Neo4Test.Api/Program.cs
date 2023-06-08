using Neo4j.Application.Repositories;
using Neo4j.Application.Services;
using Neo4j.Application.Services.Impl;
using Neo4j.Driver;
using Neo4j.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<IDriver>(serviceProvider =>
    GraphDatabase.Driver(new Uri("bolt://localhost:7687"), AuthTokens.Basic("neo4j", "Password123")));
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();