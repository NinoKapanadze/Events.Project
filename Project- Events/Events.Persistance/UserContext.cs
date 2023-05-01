using Events.Domain;
using Events.Persistance.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Event.Web.Areas.Identity.Data;

public class UserContext : IdentityDbContext<BaseUser, IdentityRole, string>
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    #region DbSets

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<BaseUser> UserS { get; set; }
    public DbSet<EvenT> Events { get; set; }

    #endregion DbSets

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserConfiguration());
    }
}