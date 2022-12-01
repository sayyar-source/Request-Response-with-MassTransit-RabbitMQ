using MassTransit;
using ResponseService.Consumers;
using ResponseService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ICatalogService, CatalogService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<ProductInfoRequestConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
       //cfg.Host(new Uri($"rabbitmq://{rabbitMqServerName}:/"),
        cfg.Host("amqp://localhost:5672",
            h =>
              {
                  h.Username("guest");
                  h.Password("guest");
              });
        cfg.ReceiveEndpoint("product-queue", c =>
        {
            c.ConfigureConsumer<ProductInfoRequestConsumer>(context);
        });
    });
});

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
