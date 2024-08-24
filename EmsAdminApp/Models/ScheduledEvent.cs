namespace EmsAdminApp.Models
{
    public class ScheduledEvent
    {
        public Guid Id { get; set; }
        public string? ImageURL { get; set; }
        public DateTime TimeSlot { get; set; }
        public string? Location { get; set; }
    }
}
