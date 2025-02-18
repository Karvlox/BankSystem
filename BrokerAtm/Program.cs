// filepath: /path/to/other/microservice/Program.cs
using AtmService.Models;
using AtmService.Repositories;
using AtmService.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICrudRepository<AtmStatus>, LogGmailRepository>();
builder.Services.AddHttpClient<LogGmailRepository>();
builder.Services.AddTransient<ICrudRepository<Atm>, JsonAtmRepository>();
builder.Services.AddHostedService<TimedHostedService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AtmEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(Environment.GetEnvironmentVariable("RabbitMQ__HostName"), "/", h =>
        {
            h.Username(Environment.GetEnvironmentVariable("RabbitMQ__Username"));
            h.Password(Environment.GetEnvironmentVariable("RabbitMQ__Password"));
        });

        cfg.ReceiveEndpoint("my-event-queue", e =>
        {
            e.ConfigureConsumer<AtmEventConsumer>(context);
        });
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();