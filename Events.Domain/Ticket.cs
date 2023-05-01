namespace Events.Domain
{
    public enum EnumStatus
    {
        free,
        reserved,
        bought
    }

    public class Ticket
    {
        public int Id { get; set; }

        public EnumStatus Status { get; set; }
        public int EvenTId { get; set; }
        public string EventName { get; set; }
        public EvenT Event { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}