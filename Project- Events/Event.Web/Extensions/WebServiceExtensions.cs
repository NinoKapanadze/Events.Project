using Event.Web;
using Events.Application;
using Events.Application.Eventz;
using Events.Application.Tickets;
using Events.Application.Userz;

//using Events.Application;
//using Events.Application.Userz;
//using Events.Infrastructure;
//using Microsoft.VisualBasic;
using Events.Infrastructure;

namespace Events.Web.Extensions
{
    public static class WebServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            #region scoped

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();

            #endregion scoped

            #region policies

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
                options.AddPolicy(Constants.Policies.RequireManager, policy => policy.RequireRole(Constants.Roles.Manager));
            });

            #endregion policies
        }
    }
}