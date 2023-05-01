namespace Events.Application.Eventz
{
    public class EventRequestModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfTicket { get; set; }
    }
}