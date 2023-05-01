// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Event.Worker;
using Event.Worker.BackgroundWorker;
using Events.Application.Eventz;
using Events.Application.Tickets;
using Events.Infrastructure;
using Events.Application.Userz;
using Events.Application;
using Event.Web.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Events.Persistance;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    await CreateHostBuilder(args).Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed");
}
finally
{
    await Log.CloseAndFlushAsync();
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    services.AddScoped<IEventService, EventService>();
    services.AddScoped<IEventRepository, EventRepository>();
    services.AddScoped<ITicketRepository, TicketRepository>();
    services.AddScoped<ITicketService, TicketService>();
        services.Configure<ConnectionStrings>(hostContext.Configuration.GetSection(nameof(ConnectionStrings.DefaultConnection)));
    services.AddDbContext<UserContext>(options =>
   options.UseSqlServer(hostContext.Configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection) ?? throw new Exception("something not right"))));
       
        services.AddHostedService<MyEventWorker>();
    })
    .UseSerilog();
