using Event.Web.Areas.Identity.Data;

namespace Events.Domain
{
    public class EvenT
    {
        public int Id { get; set; }
        public string? BaseUserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfTicket { get; set; }
        public BaseUser? User { get; set; }
        public List<Ticket> Tickets { get; set; }
        public bool AdministratorApproved { get; set; }
        public bool IsArchived { get; set; } = false;
    }
}