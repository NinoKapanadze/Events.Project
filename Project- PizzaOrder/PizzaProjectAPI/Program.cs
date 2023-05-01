using Microsoft.EntityFrameworkCore;
using PizzaProject.Persistemce;
using PizzaProject.Persistemce.Context;
using PizzaProject.Persistemce.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//aq unda iyos fluentvalidaciebi

builder.Services.AddDbContext<PizzaProjectContext> (options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection))));

// aq unda iyos mapping

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

PizzaProjectSeed.Initialize(app.Services);

app.Run();
