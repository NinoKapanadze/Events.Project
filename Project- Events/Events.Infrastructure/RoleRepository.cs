using Event.Web.Areas.Identity.Data;
using Events.Application;
using Microsoft.AspNetCore.Identity;

namespace Events.Infrastructure
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserContext _context;

        public RoleRepository(UserContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}