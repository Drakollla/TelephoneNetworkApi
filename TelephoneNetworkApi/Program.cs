using Microsoft.EntityFrameworkCore;
using TelephoneNetworkApi.Domain.Repositories;
using TelephoneNetworkApi.Domain.Services;
using TelephoneNetworkApi.Persistence;
using TelephoneNetworkApi.Persistence.Repositories;
using TelephoneNetworkApi.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TelephoneNetworDB;Trusted_Connection=True;");
});

builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<ISubscriberService, SubScriberService>();

builder.Services.AddScoped<IAutomaticTelephoneExchangeRepository, AutomaticTelephoneExchangeRepository>();
builder.Services.AddScoped<IAutomaticTelephoneExchangeService, AutomaticTelephoneExchangeService>();

builder.Services.AddScoped<IRegistrySubscriptionPaymentRepository, RegistrySubscriptionPaymentRepository>();
builder.Services.AddScoped<IRegistrySubscriptionPaymentService, RegistrySubscriptionPaymentService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
