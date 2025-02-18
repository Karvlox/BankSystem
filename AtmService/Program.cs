using MassTransit;
using AtmService.Services;
using AtmService.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAtmService, AtmService.Services.AtmService>();
builder.Services.AddTransient<ICredentialRepository, JsonCredentialsRepository>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(Environment.GetEnvironmentVariable("RabbitMQ__HostName"), "/", h =>
        {
            h.Username(Environment.GetEnvironmentVariable("RabbitMQ__Username"));
            h.Password(Environment.GetEnvironmentVariable("RabbitMQ__Password"));
        });
    });
});

builder.Services.AddHostedService<TimedHostedService>();

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
