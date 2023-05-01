//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace Events.Application
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}