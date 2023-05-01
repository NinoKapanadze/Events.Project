// See https://aka.ms/new-console-template for more information
using Event.Web.Areas.Identity.Data;
using Events.Application;

//using Event.Worker;
//using Event.Worker.BackgroundWorker;
using Events.Application.Eventz;
using Events.Application.Tickets;
using Events.Application.Userz;
using Events.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyEvent.Worker.BackgroundWorker;
using Serilog;

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
    Log.Information("Host done");
    await Log.CloseAndFlushAsync();
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<DbContext, UserContext>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IEventService, EventService>();

        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IUserService, UserService>();
        // services.Configure<ConnectionStrings>(hostContext.Configuration.GetSection(nameof(ConnectionStrings.DefaultConnection)));

        services.AddHostedService<MyEventWorker>();
    })
    .UseSerilog();