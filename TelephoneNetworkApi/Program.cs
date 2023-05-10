using Microsoft.EntityFrameworkCore;
using TelephoneNetworkApi;
using TelephoneNetworkApi.Persistence;
using TelephoneNetworkApi.Persistence.Repositories;
using TelephoneNetworkApi.Repozitories;
using TelephoneNetworkApi.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
});

builder.Services.AddScoped<ISubscriberRepositiry, SubscriberRepository>();
builder.Services.AddScoped<ISubscriberService, SubScriberService>();

//builder.Services.AddAutoMapper();


// Add services to the container.

builder.Services.AddControllers();
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
