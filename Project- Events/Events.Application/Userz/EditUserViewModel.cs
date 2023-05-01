using Event.Web.Areas.Identity.Data;
using System.Web.Mvc;

namespace Events.Application.Userz
{
    public class EditUserViewModel
    {
        public BaseUser User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}