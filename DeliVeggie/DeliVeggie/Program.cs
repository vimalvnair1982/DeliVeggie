using DeliVeggie.Data;
using DeliVeggie.RabbitMQ;
using DeliVeggieProducer.Model;
using DeliVeggieProducer.Services;
using MassTransit;
using MassTransit.Futures.Contracts;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string corsPolicyName = "ApiCORS";
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddScoped<IEasyNetSubscribe, EasyNetSubscribe>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.WithOrigins("https://localhost:7237");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
