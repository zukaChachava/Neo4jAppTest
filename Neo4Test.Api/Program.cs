using Neo4j.Application.Repositories;
using Neo4j.Application.Services;
using Neo4j.Application.Services.Impl;
using Neo4j.Infrastructure.Repositories;using Neo4jClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IBoltGraphClient, BoltGraphClient>(serviceProvider =>
{
    var client = new BoltGraphClient("neo4j://localhost:7687", "neo4j", "Password123");
    client.ConnectAsync().Wait();
    return client;
});

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();