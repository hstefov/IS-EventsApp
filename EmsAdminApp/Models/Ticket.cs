namespace EmsAdminApp.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public Guid ScheduledEventId { get; set; }
        public ScheduledEvent? ScheduledEvent { get; set; }
    }
}
