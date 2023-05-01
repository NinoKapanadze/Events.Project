using Event.Web.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Events.Persistance.Seed
{
    public static class UserSeed
    {
        // private static readonly UserContext _context;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<UserContext>();
            if (_context.Database.GetPendingMigrations().Count() > 0)
            {
                _context.Database.Migrate();
                Migrate(_context);
            }
            //if (!_roleInManager.RoleExistsAsync(Roles.Administrator.ToString()).GetAwaiter().GetResult())
            //{
            //    _roleInManager.CreateAsync(new IdentityRole(Roles.User.ToString())).GetAwaiter().GetResult();
            //    _roleInManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString())).GetAwaiter().GetResult();
            //    _roleInManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString())).GetAwaiter().GetResult();
            //}

            if (_context.Users.FirstOrDefault(x => x.UserName == "nino2000kapanadze@gmail.com") is null)
            {
                SeedEverything(_context);
            }
            //if (_context.TimeSettings.SingleOrDefaultAsync().GetAwaiter().GetResult() is null)
            //{
            //    SeedDefaultTimeSettings();
            //}
        }

        private static void Migrate(UserContext context)
        {
            context.Database.Migrate();
        }

        private static void SeedEverything(UserContext context)
        {
            var seeded = false;

            SeedUsers(context, ref seeded);

            if (seeded)
                context.SaveChanges();
        }

        private static void SeedUsers(UserContext context, ref bool seeded)
        {
            var users = new List<BaseUser>()
            {
                new BaseUser
                {
                    UserName = "nino2000kapanadze@gmail.com",
                    PasswordHash = "fdf405a5-0b27-441a-8be3-80384b39004d",
                    Role = BaseUser.EnumRole.Administrator
                }
            };
            foreach (var user in users)
            {
                if (context.Users.Any(x => x.Id == user.Id)) continue;

                context.UserS.Add(user);

                seeded = true;
            }
        }
    }
}