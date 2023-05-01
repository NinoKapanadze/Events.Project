using Events.Domain;
using Microsoft.AspNetCore.Identity;

namespace Event.Web.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class BaseUser : IdentityUser
{
    //public int Id;

    // public override string Id { get; set; }
    public List<EvenT>? Events { get; set; }

    public List<Ticket>? Tickets { get; set; }

    public enum EnumRole
    {
        User,
        Manager,
        Administrator
    }

    public EnumRole Role { get; set; } = EnumRole.User;
}