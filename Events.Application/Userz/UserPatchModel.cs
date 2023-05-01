using static Event.Web.Areas.Identity.Data.BaseUser;

namespace Events.Application.Userz
{
    public class UserPatchModel
    {
        public EnumRole Role { get; set; }
    }
}